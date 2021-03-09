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
            var employee = await _employeeRepository.GetByDocumentNumber(registerRequestModel.DocumentNumber);

            if (employee == null)
            {
                throw new KeyNotFoundException();
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

                var record = RecordEntity.Create(employee.Id, true, string.Empty);

                await _recordRepository.Create(record);
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