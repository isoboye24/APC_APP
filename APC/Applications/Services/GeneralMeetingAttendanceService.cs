using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Infrastructure.Data;
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
        private readonly IGenderRepository _genderRepository;
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;
        public GeneralMeetingAttendanceService(IGeneralMeetingAttendanceRepository repository, IMemberRepository memberRepository, 
            IGenderRepository genderRepository, IAttendanceStatusRepository attendanceStatusRepository,
            IGeneralMeetingRepository generalMeetingRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
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
                        join m in _memberRepository.GetAll() on p.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join a in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals a.attendanceStatusID
                        select new
                        {
                            p.attendanceID,
                            p.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            p.attendanceStatusID,
                            a.attendanceStatus,
                            p.monthlyDues,
                            g.genderName,
                            p.generalAttendanceID,
                        })
                        .ToList();

            return data.Select(x => new GeneralMeetingAttendanceDTO
            {
                PersonalAttendanceId = x.attendanceID,
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                AttendanceStatusId = x.attendanceStatusID,
                AttendanceStatus = x.attendanceStatus,
                DuesPaid = (decimal)x.monthlyDues,
                Gender = x.genderName,
                GeneralMeetingId = x.generalAttendanceID,
            })
            .OrderByDescending(x => x.DuesPaid).ThenByDescending(x => x.AttendanceStatus).ThenBy(x => x.FirstName).ToList();
        }
        public List<GeneralMeetingAttendanceDTO> GetMemberPersonalAttendanceByYear(int memberId, int year)
        {
            var data = (from p in _repository.GetAll().Where(x => x.memberID == memberId && x.year == year)
                        join m in _memberRepository.GetAll() on p.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join a in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals a.attendanceStatusID
                        select new
                        {
                            p.attendanceID,
                            p.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            p.attendanceStatusID,
                            a.attendanceStatus,
                            p.monthlyDues,
                            g.genderName,
                            p.generalAttendanceID,
                        })
                        .ToList();

            return data.Select(x => new GeneralMeetingAttendanceDTO
            {
                PersonalAttendanceId = x.attendanceID,
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                AttendanceStatusId = x.attendanceStatusID,
                AttendanceStatus = x.attendanceStatus,
                DuesPaid = (decimal)x.monthlyDues,
                Gender = x.genderName,
                GeneralMeetingId = x.generalAttendanceID,
            })
            .OrderByDescending(x => x.DuesPaid).ThenByDescending(x => x.AttendanceStatus).ThenBy(x => x.FirstName).ToList();
        }

        public List<GeneralMeetingAttendanceDTO> GetAllDeletedPersonalAttendance()
        {
            var data = (from p in _repository.GetAllDeletedPersonalAttendance()
                        join m in _memberRepository.GetAll() on p.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join a in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals a.attendanceStatusID
                        select new
                        {
                            p.attendanceID,
                            p.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            p.attendanceStatusID,
                            a.attendanceStatus,
                            p.monthlyDues,
                            g.genderName,
                            p.generalAttendanceID,
                        })
                        .ToList();

            return data.Select(x => new GeneralMeetingAttendanceDTO
            {
                PersonalAttendanceId = x.attendanceID,
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                AttendanceStatusId = x.attendanceStatusID,
                AttendanceStatus = x.attendanceStatus,
                DuesPaid = (decimal)x.monthlyDues,
                Gender = x.genderName,
                GeneralMeetingId = x.generalAttendanceID,
            })
            .OrderByDescending(x => x.DuesPaid).ThenByDescending(x => x.AttendanceStatus).ThenBy(x => x.FirstName).ToList();
        }

        public GeneralMeetingAttendanceDTO GetPersonalAttendanceById(int memberId, int generalMeetingId)
        {
            return (from p in _repository.GetAllByGeneralMeetingId(generalMeetingId)
                    where p.memberID == memberId
                    join m in _memberRepository.GetAll() on p.memberID equals m.memberID
                    join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                    join a in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals a.attendanceStatusID
                    select new GeneralMeetingAttendanceDTO
                    {
                        PersonalAttendanceId = p.attendanceID,
                        MemberId = p.memberID,
                        FirstName = m.name,
                        LastName = m.surname,
                        ImagePath = m.imagePath,
                        AttendanceStatusId = p.attendanceStatusID,
                        AttendanceStatus = a.attendanceStatus,
                        DuesPaid = p.monthlyDues ?? 0,
                        Gender = g.genderName,
                        GeneralMeetingId = p.generalAttendanceID,
                    })
            .FirstOrDefault();
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

            else
            {
                return _repository.Update(data);                
            }
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
