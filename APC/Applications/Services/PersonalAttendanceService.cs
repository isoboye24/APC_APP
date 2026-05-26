using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class PersonalAttendanceService : IPersonalAttendanceService
    {
        private readonly IGeneralMeetingAttendanceRepository _repository;
        private readonly IGeneralMeetingRepository _generalMeetingRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IAttendanceStatusRepository _statusRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public PersonalAttendanceService(IGeneralMeetingAttendanceRepository repository, IMemberRepository memberRepository,
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

        public int GetAnnualMembersAbsentCountById(int memberId, int year)
        {
            string status = "Absent";

            int data = (from a in _repository.GetAll().Where(x => x.memberID == memberId && x.year == year)
                        join m in _memberRepository.GetAll() on a.memberID equals m.memberID
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            m.memberID,
                        })
                        .Count();

            return data;
        }

        public int GetAnnualMembersPresentCountById(int memberId, int year)
        {
            string status = "Present";

            int data = (from a in _repository.GetAll().Where(x => x.memberID == memberId && x.year == year)
                        join m in _memberRepository.GetAll() on a.memberID equals m.memberID
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            m.memberID,
                        })
                        .Count();

            return data;
        }

        public int GetTotalMembersAbsentCountById(int memberId)
        {
            string status = "Absent";

            int data = (from a in _repository.GetAll().Where(x => x.memberID == memberId)
                        join m in _memberRepository.GetAll() on a.memberID equals m.memberID
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            m.memberID,
                        })
                        .Count();

            return data;
        }

        public int GetTotalMembersPresentCountById(int memberId)
        {
            string status = "Present";

            int data = (from a in _repository.GetAll().Where(x => x.memberID == memberId)
                        join m in _memberRepository.GetAll() on a.memberID equals m.memberID
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            m.memberID,
                        })
                        .Count();

            return data;
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
                                p.year,
                                p.monthID,
                            })
                        .ToList();

            return data.Select(x => new PersonalAttendanceDetailsDTO
            {
                MonthId = x.monthID,
                Month = GeneralHelper.ConventIntToMonth(x.monthID),
                Year = x.year,
                AttendanceStatus = x.attendanceStatus,
                MonthlyDues = x.monthlyDues ?? 0,

            })
            .OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthId)
            .ToList();
        }

        public List<PersonalAttendanceDetailsDTO> GetAnnualGeneralMeetingAttendanceById(int memberId, int year)
        {
            var data = (from p in _repository.GetAll().Where(x => x.year == year)
                        join m in _memberRepository.GetAll().Where(x => x.memberID == memberId) on p.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join ats in _attendanceStatusRepository.GetAll() on p.attendanceStatusID equals ats.attendanceStatusID
                        join gen in _generalMeetingRepository.GetAll() on p.generalAttendanceID equals gen.generalAttendanceID
                        select new
                        {
                            p.monthlyDues,
                            ats.attendanceStatus,
                            p.year,
                            p.monthID,
                        })
                    .ToList();

            return data.Select(x => new PersonalAttendanceDetailsDTO
            {
                MonthId = x.monthID,
                Month = GeneralHelper.ConventIntToMonth(x.monthID),
                Year = x.year,
                AttendanceStatus = x.attendanceStatus,
                MonthlyDues = x.monthlyDues ?? 0,

            })
            .OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthId)
            .ToList();
        }

    }
}
