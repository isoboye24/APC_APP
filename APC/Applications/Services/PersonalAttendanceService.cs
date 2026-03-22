using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class PersonalAttendanceService : IPersonalAttendanceService
    {
        private readonly IPersonalAttendanceRepository _repository;
        public PersonalAttendanceService(IPersonalAttendanceRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(PersonalAttendance data)
        {
            if (_repository.Exists(data.MemberId, data.GeneralMeetingId))
                throw new Exception("Member already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<PersonalAttendance> GetAll()
            => _repository.GetAll();

        public List<PersonalAttendance> GetAllDeleted()
            => _repository.GetAllDeleted();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public List<PersonalAttendanceFullDetails> GetFullMemberDetails()
            => _repository.GetFullPersonalAttendanceDetails();

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(PersonalAttendance data)
        {
            var member = _repository.GetById(data.GeneralMeetingId);

            if (member == null)
                throw new InvalidOperationException("Member not found");

            if (data.MonthlyDues.HasValue)
                member.UpdateMonthlyDues(data.MonthlyDues.Value);

            member.UpdateMembers(data.MemberId);
            member.UpdateAttendanceStatus(data.AttendanceStatusId);

            return _repository.Update(member);
        }
    }
}
