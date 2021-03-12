using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Exceptions;
using Veox.Attendance.Record.Application.Extensions;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Implementations.Common;
using Veox.Attendance.Record.Application.Services.Interfaces;
using Veox.Attendance.Record.Application.Validators;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;

namespace Veox.Attendance.Record.Application.Services.Implementations
{
    public class RecordService : BaseService, IRecordService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRecordRepository _recordRepository;

        public RecordService(IEmployeeRepository employeeRepository, IRecordRepository recordRepository)
        {
            _employeeRepository = employeeRepository;
            _recordRepository = recordRepository;
        }

        public async Task<SummaryEmployeeResponse> CreateAsync(RecordCreateRequest recordCreateRequest)
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
                throw new ApiException("No se encuentra habilitado para realizar la marcación");
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
                    var yesterdayLastSecond = DateTime.Today.AddSeconds(-1);
                    yesterdayRecord.Details.Add(DetailRecord
                        .CreateWithObservation(yesterdayLastSecond, false, ObservationType.CloseBySystem));
                    yesterdayRecord.IsPresent = false;

                    await _recordRepository.Update(yesterdayRecord.Id, yesterdayRecord);
                }

                var record = RecordEntity.Create(employee.Id, employee.TotalHours, string.Empty);

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
                    throw new ApiException("Ya ha realizado una marcación");
                }

                todayRecord.Details.Add(DetailRecord.Create(!todayRecord.IsPresent));
                todayRecord.IsPresent = !todayRecord.IsPresent;
                todayRecord.Update(string.Empty);

                await _recordRepository.Update(todayRecord.Id, todayRecord);
            }

            var totalHours = todayRecord.GetTotalHours();
            var elapsedHours = todayRecord.GetElapsedHours();
            var missingHours = todayRecord.GetMissingHours();

            summaryEmployeeResponse.Records = new List<RecordResponse>()
            {
                new RecordResponse
                {
                    IsPresent = todayRecord.IsPresent,
                    Date = todayRecord.Date.ToShortDateString(),
                    StartHour = todayRecord.GetStartHour(),
                    EndHour = todayRecord.GetEndHour(),
                    TotalHours = totalHours.ToLocalString(),
                    ElapsedHours = elapsedHours.ToLocalString(),
                    MissingHours = missingHours.ToLocalString(),
                    Details = todayRecord.Details.OrderByDescending(o => o.DateRecord)
                        .Select(o => new DetailRecordResponse
                        {
                            Hour = o.DateRecord.ToShortTimeString(),
                            IsStartHour = o.IsStartHour,
                            HasObservation = o.ObservationType != ObservationType.Ok,
                            Observation = o.Observation
                        }).ToList()
                }
            };

            return summaryEmployeeResponse;
        }

        public async Task<List<DailySummaryResponse>> GetDailySummaryAsync(
            DailySummaryRequest dailySummaryRequest)
        {
            Validate(new DailySummaryRequestValidator(), dailySummaryRequest);

            var dailySummariesResponse = new List<DailySummaryResponse>();

            var employees = await _employeeRepository.GetAllByWorksapce(dailySummaryRequest.WorkspaceId);

            var dateQuery = dailySummaryRequest.Date ?? DateTime.Today;

            foreach (var employee in employees)
            {
                var dailySummaryResponse = new DailySummaryResponse
                {
                    Name = employee.Name,
                    LastName = employee.LastName,
                    DocumentNumber = employee.DocumentNumber,
                    ImageProfile = employee.ImageProfile
                };

                var record = await _recordRepository.GetByDate(employee.Id, dateQuery);

                if (record != null)
                {
                    dailySummaryResponse.IsPresent = record.IsPresent;
                    dailySummaryResponse.Date = record.Date.ToShortDateString();
                    dailySummaryResponse.StartHour = record.GetStartHour();
                    dailySummaryResponse.EndHour = record.GetEndHour();
                    dailySummaryResponse.TotalHours = record.GetTotalHours().ToLocalString();
                    dailySummaryResponse.ElapsedHours = record.GetElapsedHours().ToLocalString();
                    dailySummaryResponse.MissingHours = record.GetMissingHours().ToLocalString();

                    dailySummaryResponse.Details = record.Details
                        .OrderByDescending(o => o.DateRecord)
                        .Select(o => new DailySummaryDetailResponse
                        {
                            Hour = o.DateRecord.ToShortTimeString(),
                            IsStartHour = o.IsStartHour
                        }).ToList();
                }
                else
                {
                    dailySummaryResponse.IsPresent = false;
                    dailySummaryResponse.Date = DateTime.Today.ToShortDateString();
                    dailySummaryResponse.StartHour = string.Empty;
                    dailySummaryResponse.EndHour = string.Empty;
                    dailySummaryResponse.TotalHours = employee.GetTotalHours().ToLocalString();
                    dailySummaryResponse.ElapsedHours = TimeSpan.Zero.ToLocalString();
                    dailySummaryResponse.MissingHours = employee.GetTotalHours().ToLocalString();
                }

                dailySummariesResponse.Add(dailySummaryResponse);
            }

            return dailySummariesResponse;
        }

        public async Task<SummaryEmployeeResponse> GetSummaryByEmployeeAsync(
            SummaryEmployeeRequest summaryEmployeeRequest)
        {
            var summaryEmployeeResponse = new SummaryEmployeeResponse();

            // This code section add the employee info to response.

            var employee = await _employeeRepository.GetById(summaryEmployeeRequest.EmployeeId);

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

            var startDate = summaryEmployeeRequest.StartDate ?? DateTime.Today;
            var endDate = summaryEmployeeRequest.EndDate ?? DateTime.Today;

            if (startDate > endDate || endDate > DateTime.Today)
            {
                throw new ApiException("El intervalo de fechas para esta consulta es incorrecto");
            }

            var resultRecords =
                await _recordRepository.GetSummaryByDate(summaryEmployeeRequest.EmployeeId, startDate, endDate);

            var records = resultRecords
                .OrderByDescending(x => x.Date)
                .ToList();

            summaryEmployeeResponse.Records = records
                .Select(x => new RecordResponse
                {
                    IsPresent = x.IsPresent,
                    Date = x.Date.ToShortDateString(),
                    StartHour = x.GetStartHour(),
                    EndHour = x.GetEndHour(),
                    TotalHours = x.GetTotalHours().ToLocalString(),
                    ElapsedHours = x.GetElapsedHours().ToLocalString(),
                    MissingHours = x.GetMissingHours().ToLocalString(),
                    Details = x.Details
                        .OrderByDescending(o => o.DateRecord)
                        .Select(o => new DetailRecordResponse
                        {
                            Hour = o.DateRecord.ToShortTimeString(),
                            IsStartHour = o.IsStartHour,
                            HasObservation = o.ObservationType != ObservationType.Ok,
                            Observation = o.Observation
                        }).ToList()
                }).ToList();

            var totalHours = CalculateTotalHours(startDate, endDate, employee.TotalHours);
            var totalElapsedHours = GetTotalElapsedHours(records);
            var totalMissingHours = CalculateMissingHours(totalHours, totalElapsedHours);

            summaryEmployeeResponse.TotalHours = totalHours.ToLocalString();
            summaryEmployeeResponse.ElapsedHours = totalElapsedHours.ToLocalString();
            summaryEmployeeResponse.MissingHours = totalMissingHours.ToLocalString();

            return summaryEmployeeResponse;
        }

        private static TimeSpan GetTotalElapsedHours(IEnumerable<RecordEntity> records)
        {
            var elapsedHours = records.Sum(x => x.GetElapsedHours().Ticks);
            return TimeSpan.FromTicks(elapsedHours);
        }

        private static TimeSpan CalculateTotalHours(DateTime startDate, DateTime endDate, int totalDailyHours)
        {
            var totalDays = endDate.Subtract(startDate).Days;
            var totalHours = totalDailyHours * totalDays;
            return TimeSpan.FromHours(totalHours);
        }

        private static TimeSpan CalculateMissingHours(TimeSpan totalHours, TimeSpan elapsedHours)
        {
            return totalHours.Subtract(elapsedHours);
        }
    }
}