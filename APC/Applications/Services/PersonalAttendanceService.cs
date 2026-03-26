using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class PersonalAttendanceService : IPersonalAttendanceService
    {
        private readonly IPersonalAttendanceRepository _repository;
        private readonly IMemberRepository _memberRepository;
        private readonly IAttendanceStatusRepository _statusRepository;
        private readonly IGenderRepository _genderRepository;
        public PersonalAttendanceService(IPersonalAttendanceRepository repository, IMemberRepository memberRepository, 
            IAttendanceStatusRepository statusRepository, IGenderRepository genderRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _statusRepository = statusRepository;
            _genderRepository = genderRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Domain.Entities.PersonalAttendance data)
        {
            if (_repository.Exists(data.MemberId, data.GeneralMeetingId))
                throw new Exception("Member already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<PersonalAttendanceDTO> GetAllByGeneralMeetingId(int id)
        {
            var data = (from p in _repository.GetAllByGeneralMeetingId(id)
                        join m in _memberRepository.GetAll() on p.memberID equals m.MemberId
                        join g in _genderRepository.GetAll() on m.PersonalInfo.GenderId equals g.genderID
                        join a in _statusRepository.GetAll() on p.attendanceStatusID equals a.AttendanceStatusId
                        select new PersonalAttendanceDTO
                        {
                            PersonalAttendanceId = p.attendanceID,
                            MemberId = p.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            AttendanceStatusId = p.attendanceStatusID,
                            AttendanceStatus = a.AttendanceStatusName,
                            DuesPaid = (decimal)p.monthlyDues,
                            Gender = g.genderName,
                            GeneralMeetingId = p.generalAttendanceID,
                        }).ToList();

            data = data.Select((x, index) =>
            {
                x.Counter = index + 1;
                return x;
            }).OrderByDescending(x => x.DuesPaid).ThenByDescending(x => x.AttendanceStatus).ThenBy(x => x.FirstName).ToList();

            return data;
        }

        public List<PersonalAttendanceDTO> GetAllDeletedPersonalAttendance()
        {
            var data = (from p in _repository.GetAllDeletedPersonalAttendance()
                        join m in _memberRepository.GetAll() on p.memberID equals m.MemberId
                        join g in _genderRepository.GetAll() on m.PersonalInfo.GenderId equals g.genderID
                        join a in _statusRepository.GetAll() on p.attendanceStatusID equals a.AttendanceStatusId
                        select new PersonalAttendanceDTO
                        {
                            PersonalAttendanceId = p.attendanceID,
                            MemberId = p.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            AttendanceStatusId = p.attendanceStatusID,
                            AttendanceStatus = a.AttendanceStatusName,
                            DuesPaid = (decimal)p.monthlyDues,
                            Gender = g.genderName,
                            GeneralMeetingId = p.generalAttendanceID,
                        }).ToList();

            data = data.Select((x, index) =>
            {
                x.Counter = index + 1;
                return x;
            }).OrderByDescending(x => x.DuesPaid).ThenByDescending(x => x.AttendanceStatus).ThenBy(x => x.FirstName).ToList();

            return data;
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Domain.Entities.PersonalAttendance data)
        {
            var member = _repository.GetById(data.GeneralMeetingId);

            if (member == null)
                throw new InvalidOperationException("Member not found");

            if (data.MonthlyDues.HasValue)
                data.UpdateMonthlyDues(data.MonthlyDues.Value);

            data.UpdateMembers(data.MemberId);
            data.UpdateAttendanceStatus(data.AttendanceStatusId);

            return _repository.Update(data);
        }
    }
}
