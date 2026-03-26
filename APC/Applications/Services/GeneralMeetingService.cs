using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;


namespace APC.Applications.Services
{
    public class GeneralMeetingService : IGeneralMeetingService
    {
        private readonly IGeneralMeetingRepository _repository;
        public GeneralMeetingService(IGeneralMeetingRepository repository)
        {
            _repository = repository;
        }

        public int Count()
        => _repository.Count();

        public bool Create(GeneralMeeting data)
        {
            if (_repository.Exists(data.GeneralMeetingDate.Month, data.GeneralMeetingDate.Year))
                throw new Exception("Meeting already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<GeneralMeeting> GetAll()
            => _repository.GetAll();

        public List<GeneralMeeting> GetAllDeleted()
            => _repository.GetAllDeleted();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(GeneralMeeting data)
        {
            var meeting = _repository.GetById(data.GeneralMeetingId);
            if (meeting == null)
                throw new InvalidOperationException("Meeting not found");

            if (data.TotalMembersPresent.HasValue)
                data.UpdateTotalMembersPresent(data.TotalMembersPresent.Value);

            if (data.TotalMembersAbsent.HasValue)
                data.UpdateTotalMembersAbsent(data.TotalMembersAbsent.Value);

            if (data.TotalDuesPaid.HasValue)
                data.UpdateTotalDuesPaid(data.TotalDuesPaid.Value);

            if (data.TotalDuesExpected.HasValue)
                data.UpdateTotalDuesExpected(data.TotalDuesExpected.Value);

            if (data.TotalDuesBalance.HasValue)
                data.UpdateTotalDuesBalance(data.TotalDuesBalance.Value);

            if (!string.IsNullOrWhiteSpace(data.Summary))
                data.UpdateSummary(data.Summary);

            data.UpdateGeneralMeetingDate(data.GeneralMeetingDate);

            return _repository.Update(data);
        }
    }
}
