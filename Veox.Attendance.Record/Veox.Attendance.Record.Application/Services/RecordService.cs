using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Interfaces.Services;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Wrappers;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;

namespace Veox.Attendance.Record.Application.Services
{
    public class RecordService : IRecordService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRecordRepository _recordRepository;

        public RecordService(IEmployeeRepository employeeRepository, IRecordRepository recordRepository)
        {
            _employeeRepository = employeeRepository;
            _recordRepository = recordRepository;
        }

        public async Task<Response<SummaryEmployeeResponse>> CreateAsync(RecordCreateRequest recordCreateRequest)
        {
            var summaryEmployeeResponse = new SummaryEmployeeResponse();

            // This code section add the employee info to response.

            var employee = await _employeeRepository.GetByDocumentNumberAndWorkspace(
                recordCreateRequest.DocumentNumber, recordCreateRequest.WorkspaceId);

            if (employee == null)
            {
                throw new KeyNotFoundException("No se encuentra registrado en este espacio de trabajo");
            }

            if (!employee.IsEnabled)
            {
                throw new KeyNotFoundException("No se encuentra habilitado para realizar la marcación");
            }

            var employeeResponse = new EmployeeResponse
            {
                Name = employee.Name,
                LastName = employee.LastName,
                DocumentNumber = employee.DocumentNumber,
                ImageProfile = employee.ImageProfile
            };

            summaryEmployeeResponse.Employee = employeeResponse;

            // This code section create a new record.

            var todayRecord = await _recordRepository.GetByDate(employee.Id, DateTime.Today);

            if (todayRecord == null)
            {
                var yesterdayRecord = await _recordRepository.GetByDate(employee.Id, DateTime.Today.AddDays(-1));

                if (yesterdayRecord != null && yesterdayRecord.IsPresent)
                {
                    yesterdayRecord.Details.Add(DetailRecord.CreateWithObservation(ObservationType.CloseBySystem));
                    yesterdayRecord.IsPresent = false;

                    await _recordRepository.Update(yesterdayRecord.Id, yesterdayRecord);
                }

                var record = RecordEntity.Create(employee.Id, string.Empty);

                await _recordRepository.Create(record);

                todayRecord = record;
            }
            else
            {
                var lastDetail = todayRecord.Details.Last();

                var intervalDate = DateTime.Now - lastDetail.DateRecord;
                var intervalMinutes = intervalDate.Minutes;

                if (intervalMinutes <= 1)
                {
                    throw new NotSupportedException("Ya ha realizado una marcación");
                }

                todayRecord.Details.Add(DetailRecord.Create(!todayRecord.IsPresent));
                todayRecord.IsPresent = !todayRecord.IsPresent;
                todayRecord.Update(string.Empty);

                await _recordRepository.Update(todayRecord.Id, todayRecord);
            }

            summaryEmployeeResponse.Records.Add(new RecordResponse
            {
                IsPresent = todayRecord.IsPresent,
                Date = todayRecord.Date.ToShortDateString(),
                StartHour = todayRecord.GetStartHour(),
                EndHour = todayRecord.GetEndHour()
            });

            return new Response<SummaryEmployeeResponse>(summaryEmployeeResponse);
        }

        public async Task<Response<List<DailySummaryResponse>>> GetDailySummaryByWorkspaceAsync(
            DailySummaryRequest dailySummaryRequest)
        {
            var employees = await _employeeRepository.GetAllByWorksapce(dailySummaryRequest.WorkspaceId);

            var dateQuery = dailySummaryRequest.Date ?? DateTime.Today;

            var dailySummary = new List<DailySummaryResponse>();

            foreach (var employee in employees)
            {
                var record = await _recordRepository.GetByDate(employee.Id, dateQuery);

                dailySummary.Add(new DailySummaryResponse
                {
                    Name = employee.Name,
                    LastName = employee.LastName,
                    DocumentNumber = employee.DocumentNumber,
                    ImageProfile = employee.ImageProfile,
                    IsPresent = record.IsPresent,
                    Date = record.Date.ToShortDateString(),
                    StartHour = record.GetStartHour(),
                    EndHour = record.GetEndHour()
                });
            }

            return new Response<List<DailySummaryResponse>>(dailySummary);
        }

        public async Task<Response<SummaryEmployeeResponse>> GetSummaryByEmployeeAsync(
            RecordSummaryRequest recordSummaryRequest)
        {
            var summaryEmployeeResponse = new SummaryEmployeeResponse();

            // This code section add the employee info to response.

            var employee = await _employeeRepository.GetById(recordSummaryRequest.EmployeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException("No se encuentra registrado en este espacio de trabajo");
            }

            var employeeResponse = new EmployeeResponse
            {
                Name = employee.Name,
                LastName = employee.LastName,
                DocumentNumber = employee.DocumentNumber,
                ImageProfile = employee.ImageProfile
            };

            summaryEmployeeResponse.Employee = employeeResponse;

            // This code section add the record info to response.

            var startDate = recordSummaryRequest.StartDate ?? DateTime.Today;
            var endDate = recordSummaryRequest.EndDate ?? DateTime.Today;

            if (startDate > endDate)
            {
                throw new NotSupportedException("Invervalo de consulta incorrecto");
            }

            var records = await _recordRepository.GetSummaryByDate(recordSummaryRequest.EmployeeId, startDate, endDate);

            foreach (var record in records)
            {
                summaryEmployeeResponse.Records.Add(new RecordResponse
                {
                    IsPresent = record.IsPresent,
                    Date = record.Date.ToShortDateString(),
                    StartHour = record.GetStartHour(),
                    EndHour = record.GetEndHour()
                });
            }

            return new Response<SummaryEmployeeResponse>(summaryEmployeeResponse);
        }
    }
}