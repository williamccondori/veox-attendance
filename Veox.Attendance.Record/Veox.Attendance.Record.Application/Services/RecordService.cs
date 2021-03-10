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

        public async Task<Response<RecordModel>> CreateAsync(RecordCreateModel registerRequestModel)
        {
            var employee =
                await _employeeRepository.GetByDocumentNumberAndWorkspace(registerRequestModel.DocumentNumber,
                    registerRequestModel.WorkspaceId);

            if (employee == null)
            {
                throw new KeyNotFoundException("No se encuentra registrado en este espacio de trabajo");
            }

            if (!employee.IsEnabled)
            {
                throw new KeyNotFoundException("No se encuentra habilitado para realizar la marcación");
            }

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
                    return new Response<RecordModel>("Ya ha realizado una marcación");
                }

                todayRecord.Details.Add(DetailRecord.Create());
                todayRecord.IsPresent = !todayRecord.IsPresent;
                todayRecord.Update(string.Empty);

                await _recordRepository.Update(todayRecord.Id, todayRecord);
            }

            var response = new RecordModel
            {
                Name = employee.Name,
                LastName = employee.LastName,
                ImageProfile = employee.ImageProfile,
                IsPresent = todayRecord.IsPresent,
            };

            return new Response<RecordModel>(response);
        }

        public async Task<Response<IEnumerable<DailyRecordResponse>>> GetDailySummaryByWorkspace(
            DailySummaryRequest dailySummaryRequest)
        {
            var employees = await _employeeRepository.GetAllByWorksapce(dailySummaryRequest.WorkspaceId);

            var dateQuery = dailySummaryRequest.Date ?? DateTime.Today;

            var dailyRecords = new List<DailyRecordResponse>();

            foreach (var employee in employees)
            {
                var record = await _recordRepository.GetByDate(employee.Id, dateQuery);

                dailyRecords.Add(new DailyRecordResponse
                {
                    Name = employee.Name,
                    LastName = employee.LastName,
                    ImageProfile = employee.ImageProfile,
                    IsPresent = record.IsPresent,
                    Date = record.Date.ToShortDateString()
                });
            }

            return new Response<IEnumerable<DailyRecordResponse>>(dailyRecords);
        }
    }
}