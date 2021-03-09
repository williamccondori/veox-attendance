using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var employee = await _employeeRepository.GetByDocumentNumber(registerRequestModel.DocumentNumber);

            var todayRecord = await _recordRepository.GetByDate(employee.Id, DateTime.Today);

            if (todayRecord == null)
            {
                var yesterdayRecord = await _recordRepository.GetByDate(employee.Id, DateTime.Today.AddDays(-1));

                if (yesterdayRecord.IsPresent)
                {
                    yesterdayRecord.Details.Add(new DetailRecord {DateRecord = DateTime.Now});
                    yesterdayRecord.IsPresent = false;
                    yesterdayRecord.Observations.Add(new ObservationRecord
                        {ObservationType = ObservationType.CloseBySystem, Message = "Close by system"});
                    await _recordRepository.Update(yesterdayRecord.Id, yesterdayRecord);
                }

                var record = new RecordEntity
                {
                    EmployeeId = employee.Id,
                    Date = DateTime.Today,
                    Details = new List<DetailRecord>
                    {
                        new DetailRecord {DateRecord = DateTime.Now}
                    }
                };

                await _recordRepository.Create(record);
            }
            else
            {
                var lastDetail = todayRecord.Details.Last();

                var intervalDate = DateTime.Now - lastDetail.DateRecord;
                var intervalMinutes = intervalDate.Minutes;

                if (intervalMinutes <= 10)
                {
                    throw new ValidationException("Ya ha realizado una marcación");
                }

                todayRecord.Details.Add(new DetailRecord {DateRecord = DateTime.Now});
                todayRecord.IsPresent = !todayRecord.IsPresent;
                await _recordRepository.Update(todayRecord.Id, todayRecord);
            }

            var response = new RecordModel
            {
                Name = employee.Name,
                LastName = employee.LastName,
                ImageProfile = employee.ImageProfile,
                IsPresent = true
            };

            return new Response<RecordModel>(response);
        }
    }
}