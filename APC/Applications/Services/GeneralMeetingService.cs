using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                meeting.UpdateTotalMembersPresent(data.TotalMembersPresent.Value);

            if (data.TotalMembersAbsent.HasValue)
                meeting.UpdateTotalMembersAbsent(data.TotalMembersAbsent.Value);

            if (data.TotalDuesPaid.HasValue)
                meeting.UpdateTotalDuesPaid(data.TotalDuesPaid.Value);

            if (data.TotalDuesExpected.HasValue)
                meeting.UpdateTotalDuesExpected(data.TotalDuesExpected.Value);

            if (data.TotalDuesBalance.HasValue)
                meeting.UpdateTotalDuesBalance(data.TotalDuesBalance.Value);

            if (!string.IsNullOrWhiteSpace(data.Summary))
                meeting.UpdateSummary(data.Summary);

            meeting.UpdateGeneralMeetingDate(data.GeneralMeetingDate);

            return _repository.Update(meeting);
        }
    }
}
