using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using APC.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class PersonalAttendanceService : IPersonalAttendanceService
    {
        private readonly IGeneralMeetingAttendanceRepository _repository;
        private readonly IGeneralMeetingRepository _generalMeetingRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public PersonalAttendanceService(IGeneralMeetingAttendanceRepository repository, IMemberRepository memberRepository, 
            IGenderRepository genderRepository, IAttendanceStatusRepository attendanceStatusRepository, IGeneralMeetingRepository generalMeetingRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _genderRepository = genderRepository;
            _attendanceStatusRepository = attendanceStatusRepository;
            _generalMeetingRepository = generalMeetingRepository;
        }

        private int statusYearlyCount(int memberId, int year, string status)
        {
            return (from a in _repository.GetAll().Where(x => x.memberID == memberId)
                               join meeting in _generalMeetingRepository.GetAll().Where(x => x.attendanceDate.Year == year)
                               on a.generalAttendanceID equals meeting.generalAttendanceID
                               join m in _memberRepository.GetAll()
                               on a.memberID equals m.memberID
                               join ats in _attendanceStatusRepository.GetByStatus(status)
                               on a.attendanceStatusID equals ats.attendanceStatusID
                               select new
                               {
                                   m.memberID,
                               })
                        .Count();
        }
        
        private int statusTotalCount(int memberId, string status)
        {
            return (from a in _repository.GetAll().Where(x => x.memberID == memberId)
                               join meeting in _generalMeetingRepository.GetAll()
                               on a.generalAttendanceID equals meeting.generalAttendanceID
                               join m in _memberRepository.GetAll()
                               on a.memberID equals m.memberID
                               join ats in _attendanceStatusRepository.GetByStatus(status)
                               on a.attendanceStatusID equals ats.attendanceStatusID
                               select new
                               {
                                   m.memberID,
                               })
                        .Count();
        }

        public int GetAnnualMembersAbsentCountById(int memberId, int year)
        {
            string status = "Absent";

            return statusYearlyCount(memberId, year, status);
        }

        public int GetAnnualMembersPresentCountById(int memberId, int year)
        {
            string status = "Present";

            return statusYearlyCount(memberId, year, status);
        }

        public int GetTotalMembersAbsentCountById(int memberId)
        {
            string status = "Absent";

            return statusTotalCount(memberId, status);
        }

        public int GetTotalMembersPresentCountById(int memberId)
        {
            string status = "Present";

            return statusTotalCount(memberId, status);
        }

        public List<PersonalAttendanceDetailsDTO> GetTotalGeneralMeetingAttendanceById(int memberId)
        {
                var data = (from p in _repository.GetAll()
                            join m in _memberRepository.GetAll().Where(x => x.memberID == memberId) on p.memberID equals m.memberID
                            join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                            join ats in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals ats.attendanceStatusID
                            join gen in _generalMeetingRepository.GetAll() on p.generalAttendanceID equals gen.generalAttendanceID
                            select new
                            {
                                p.monthlyDues,
                                ats.attendanceStatus,
                                gen.attendanceDate.Year,
                                gen.attendanceDate.Month,
                            })
                        .ToList();

            return data.Select(x => new PersonalAttendanceDetailsDTO
            {
                MonthId = x.Month,
                Month = GeneralHelper.ConventIntToMonth(x.Month),
                Year = x.Year,
                AttendanceStatus = x.attendanceStatus,
                MonthlyDues = x.monthlyDues ?? 0,

            })
            .OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthId)
            .ToList();
        }

        public List<PersonalAttendanceDetailsDTO> GetAnnualGeneralMeetingAttendanceById(int memberId, int year)
        {
            var data = (from p in _repository.GetAll()
                        join m in _memberRepository.GetAll().Where(x => x.memberID == memberId) on p.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join ats in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals ats.attendanceStatusID
                        join gen in _generalMeetingRepository.GetAll().Where(x => x.attendanceDate.Year == year) on p.generalAttendanceID equals gen.generalAttendanceID
                        select new
                        {
                            p.monthlyDues,
                            ats.attendanceStatus,
                            gen.attendanceDate.Year,
                            gen.attendanceDate.Month,
                        })
                    .ToList();

            return data.Select(x => new PersonalAttendanceDetailsDTO
            {
                MonthId = x.Month,
                Month = GeneralHelper.ConventIntToMonth(x.Month),
                Year = x.Year,
                AttendanceStatus = x.attendanceStatus,
                MonthlyDues = x.monthlyDues ?? 0,

            })
            .OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthId)
            .ToList();
        }

        public List<YearDTO> GetPersonalAttendanceYears(int memberId)
        {

            return (
                from attendance in _repository.GetAll().Where(x => x.memberID == memberId)
                join meeting in _generalMeetingRepository.GetAll()
                    on attendance.generalAttendanceID equals meeting.generalAttendanceID
                select meeting.attendanceDate.Year
            )
            .Distinct()
            .OrderByDescending(x => x)
            .Select(x => new YearDTO
            {
                YearInValue = x,
                YearInText = x.ToString()
            })
            .ToList();
        }

    }
}
