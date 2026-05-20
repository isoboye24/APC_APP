using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class GeneralMeetingAttendanceService : IGeneralMeetingAttendanceService
    {
        private readonly IGeneralMeetingAttendanceRepository _repository;
        private readonly IGeneralMeetingRepository _generalMeetingRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IAttendanceStatusRepository _statusRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;
        public GeneralMeetingAttendanceService(IGeneralMeetingAttendanceRepository repository, IMemberRepository memberRepository, 
            IAttendanceStatusRepository statusRepository, IGenderRepository genderRepository, IAttendanceStatusRepository attendanceStatusRepository,
            IGeneralMeetingRepository generalMeetingRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _statusRepository = statusRepository;
            _genderRepository = genderRepository;
            _attendanceStatusRepository = attendanceStatusRepository;
            _generalMeetingRepository = generalMeetingRepository;
        }

        public int Count()
            => _repository.Count();

        public int GetMembersPresentCount(int generalMeetingId)
        {
            string status = "Present";

            int data = (from a in _repository.GetAll().Where(x => x.generalAttendanceID == generalMeetingId)
                        join m in _memberRepository.GetAll() on a.memberID equals m.memberID
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            m.memberID,
                        })
                        .Count();

            return data;
        }
        
        public int GetMembersAbsentCount(int generalMeetingId)
        {
            string status = "Absent";

            int data = (from a in _repository.GetAll().Where(x => x.generalAttendanceID == generalMeetingId)
                        join m in _memberRepository.GetAll() on a.memberID equals m.memberID
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            m.memberID,
                        })
                        .Count();

            return data;
        }
        
        public decimal GetTotalDuesPaid(int generalMeetingId)
        {
            return _repository.GetAll().Where(x => x.generalAttendanceID == generalMeetingId).Sum(x =>x.monthlyDues ?? 0);
        }

        public bool Create(PersonalAttendance data)
        {
            if (_repository.Exists(data.MemberId, data.GeneralMeetingId))
                throw new Exception("Member already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<GeneralMeetingAttendanceDTO> GetAllByGeneralMeetingId(int id)
        {
            var data = (from p in _repository.GetAllByGeneralMeetingId(id)
                        join m in _memberRepository.GetAll() on p.memberID equals m.MemberId
                        join g in _genderRepository.GetAll() on m.PersonalInfo.GenderId equals g.genderID
                        join a in _statusRepository.GetAll() on p.attendanceStatusID equals a.attendanceStatusID
                        select new GeneralMeetingAttendanceDTO
                        {
                            PersonalAttendanceId = p.attendanceID,
                            MemberId = p.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            ImagePath = m.PersonalInfo.ImagePath,
                            AttendanceStatusId = p.attendanceStatusID,
                            AttendanceStatus = a.attendanceStatus,
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

        public List<GeneralMeetingAttendanceDTO> GetAllDeletedPersonalAttendance()
        {
            var data = (from p in _repository.GetAllDeletedPersonalAttendance()
                        join m in _memberRepository.GetAll() on p.memberID equals m.MemberId
                        join g in _genderRepository.GetAll() on m.PersonalInfo.GenderId equals g.genderID
                        join a in _statusRepository.GetAll() on p.attendanceStatusID equals a.AttendanceStatusId
                        select new GeneralMeetingAttendanceDTO
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

        public bool Update(PersonalAttendance data)
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

        public int GetLastMeetingPresentMembersCount()
        {
            var lastMeeting = _generalMeetingRepository.GetAll()
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .FirstOrDefault();

            if (lastMeeting == null)
                return 0;

            const int presentStatusId = 2;

            return _repository.GetAll()
                .Count(x =>
                    !x.isDeleted &&
                    x.year == lastMeeting.year &&
                    x.monthID == lastMeeting.monthID &&
                    x.day == lastMeeting.day &&
                    x.attendanceStatusID == presentStatusId);
        }
    }
}
