using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using APC.Infrastructure.Data;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace APC.Applications.Services
{
    public class GeneralMeetingService : IGeneralMeetingService
    {
        private readonly IGeneralMeetingRepository _repository;
        private readonly IMonthRepository _monthRepository;
        private readonly IFinedMemberRepository _finedMemberRepository;
        private readonly IGeneralMeetingAttendanceRepository _generalMeetingAttendanceRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public GeneralMeetingService(IGeneralMeetingRepository repository, IMonthRepository monthRepository, IFinedMemberRepository finedMemberRepository,
            IGeneralMeetingAttendanceRepository generalMeetingAttendanceRepository, IMemberRepository memberRepository, 
            IAttendanceStatusRepository attendanceStatusRepository)
        {
            _repository = repository;
            _monthRepository = monthRepository;
            _finedMemberRepository = finedMemberRepository;
            _generalMeetingAttendanceRepository = generalMeetingAttendanceRepository;
            _memberRepository = memberRepository;
            _attendanceStatusRepository = attendanceStatusRepository;
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

        private int GetTotalMembersAbsentCountById(int generalMeetingId)
        {
            string status = "Absent";

            int data = (from a in _generalMeetingAttendanceRepository.GetAll().Where(x => x.generalAttendanceID == generalMeetingId)
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            a.memberID,
                        })
                        .Count();

            return data;
        }

        private int GetTotalMembersPresentCountById(int generalMeetingId)
        {
            string status = "Present";

            int data = (from a in _generalMeetingAttendanceRepository.GetAll().Where(x => x.generalAttendanceID == generalMeetingId)
                        join ats in _attendanceStatusRepository.GetByStatus(status) on a.attendanceStatusID equals ats.attendanceStatusID
                        select new
                        {
                            a.memberID,
                        })
                        .Count();

            return data;
        }

        private decimal GetTotalDuesPaid(int generalMeetingId)
        {
            var data = _generalMeetingAttendanceRepository.GetAll().Where(x => x.generalAttendanceID == generalMeetingId).ToList();

            decimal total = data.Sum(x => (decimal)x.monthlyDues);

            if (total > 0)
            {
                return total;
            }
            else
            {
                return 0;
            }
        }

        public List<GeneralMeetingDTO> GetAll()
        {
            var data = (from g in _repository.GetAll()
                        join m in _monthRepository.GetAll() on g.monthID equals m.monthID
                        select new
                        {
                            g.generalAttendanceID,
                            g.totalMembersPresent,
                            g.totalMembersAbsent,
                            g.totalDuesPaid,
                            g.totalDuesExpected,
                            g.totalDuesBalance,
                            g.attendanceDate,
                            g.summary                            
                        })
                        .ToList();

            return data.Select(x => new GeneralMeetingDTO
            {
                GeneralMeetingId = x.generalAttendanceID,
                TotalMembersPresent = GetTotalMembersPresentCountById(x.generalAttendanceID),
                TotalMembersAbsent = GetTotalMembersAbsentCountById(x.generalAttendanceID),
                TotalDuesPaid = GetTotalDuesPaid(x.generalAttendanceID),
                FormattedTotalDuesPaid = GetTotalDuesPaid(x.generalAttendanceID).ToString(),

                FinesRaised = (_finedMemberRepository.GetAllByDate(x.attendanceDate).Sum(y => y.amountPaid) ?? 0).ToString(),
                Summary = x.summary,
                Day = x.attendanceDate.Day,
                MonthName = GeneralHelper.ConventIntToMonth(x.attendanceDate.Month),
                MonthId = x.attendanceDate.Month,
                Year = x.attendanceDate.Year,
                GeneralMeetingDate = x.attendanceDate,
            })
            .OrderByDescending(x => x.GeneralMeetingDate)
            .ToList();
        }

        public List<GeneralMeetingDTO> GetAllByYear(int year)
        {
            var data = (from g in _repository.GetAll().Where(g => g.attendanceDate.Year == year)
                        join m in _monthRepository.GetAll() on g.monthID equals m.monthID
                        select new
                        {
                            g.generalAttendanceID,
                            g.totalMembersPresent,
                            g.totalMembersAbsent,
                            g.totalDuesPaid,
                            g.totalDuesExpected,
                            g.totalDuesBalance,
                            g.attendanceDate,
                            g.summary
                        })
                        .ToList();

            return data.Select(x => new GeneralMeetingDTO
            {
                GeneralMeetingId = x.generalAttendanceID,
                TotalMembersPresent = GetTotalMembersPresentCountById(x.generalAttendanceID),
                TotalMembersAbsent = GetTotalMembersAbsentCountById(x.generalAttendanceID),
                TotalDuesPaid = GetTotalDuesPaid(x.generalAttendanceID),
                FormattedTotalDuesPaid = GetTotalDuesPaid(x.generalAttendanceID).ToString(),

                FinesRaised = (_finedMemberRepository.GetAllByDate(x.attendanceDate).Sum(y => y.amountPaid) ?? 0).ToString(),
                Summary = x.summary,
                Day = x.attendanceDate.Day,
                MonthName = GeneralHelper.ConventIntToMonth(x.attendanceDate.Month),
                MonthId = x.attendanceDate.Month,
                Year = x.attendanceDate.Year,
                GeneralMeetingDate = x.attendanceDate,
            })
            .OrderByDescending(x => x.GeneralMeetingDate)            
            .ToList();
        }

        public List<GeneralMeetingDTO> GetAllDeleted()
        {
            var data = (from g in _repository.GetAllDeletedGeneralMeetings()
                        join m in _monthRepository.GetAll() on g.monthID equals m.monthID
                        select new
                        {
                            g.generalAttendanceID,
                            g.totalMembersPresent,
                            g.totalMembersAbsent,
                            g.totalDuesPaid,
                            g.totalDuesExpected,
                            g.totalDuesBalance,
                            g.attendanceDate,
                            g.summary
                        })
                        .ToList();

            return data.Select(x => new GeneralMeetingDTO
            {
                GeneralMeetingId = x.generalAttendanceID,
                TotalMembersPresent = GetTotalMembersPresentCountById(x.generalAttendanceID),
                TotalMembersAbsent = GetTotalMembersAbsentCountById(x.generalAttendanceID),
                TotalDuesPaid = GetTotalDuesPaid(x.generalAttendanceID),
                FormattedTotalDuesPaid = GetTotalDuesPaid(x.generalAttendanceID).ToString(),

                FinesRaised = (_finedMemberRepository.GetAllByDate(x.attendanceDate).Sum(y => y.amountPaid) ?? 0).ToString(),
                Summary = x.summary,
                Day = x.attendanceDate.Day,
                MonthName = GeneralHelper.ConventIntToMonth(x.attendanceDate.Month),
                MonthId = x.attendanceDate.Month,
                Year = x.attendanceDate.Year,
                GeneralMeetingDate = x.attendanceDate,
            })
            .OrderByDescending(x => x.GeneralMeetingDate)
            .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(GeneralMeeting data)
        {
            var meeting = _repository.GetById(data.GeneralMeetingId);
            if (meeting == null)
                throw new InvalidOperationException("Meeting not found");

            else
            {
                return _repository.Update(data);               
            }
        }

        public List<YearDTO> GetMeetingYears()
        {
            return _repository.GetAll()
                .Where(x => !x.isDeleted)
                .Select(x => x.attendanceDate.Year)
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
