using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using APC.Applications.DTO;
using System.Linq;
using APC.Helper;


namespace APC.Applications.Services
{
    public class GeneralMeetingService : IGeneralMeetingService
    {
        private readonly IGeneralMeetingRepository _repository;
        private readonly IMonthRepository _monthRepository;
        private readonly IFinedMemberRepository _finedMemberRepository;
        public GeneralMeetingService(IGeneralMeetingRepository repository, IMonthRepository monthRepository, IFinedMemberRepository finedMemberRepository)
        {
            _repository = repository;
            _monthRepository = monthRepository;
            _finedMemberRepository = finedMemberRepository;
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
                TotalMembersPresent = (int)x.totalMembersPresent,
                TotalMembersAbsent = (int)x.totalMembersAbsent,
                TotalDuesPaid = (decimal)x.totalDuesPaid,
                FormattedTotalDuesPaid = ((decimal)x.totalDuesPaid).ToString(),
                TotalDuesExpected = (decimal)x.totalDuesExpected,
                FormattedTotalDuesExpected = ((decimal)x.totalDuesExpected).ToString(),
                TotalDuesBalance = ((decimal)x.totalDuesExpected - (decimal)x.totalDuesPaid).ToString(),
                FinesRaised = _finedMemberRepository.GetAllByDate(x.attendanceDate).Sum(y => y.amountPaid).ToString(),
                Summary = x.summary,
                MonthName = GeneralHelper.ConventIntToMonth(x.attendanceDate.Month),
                MonthId = x.attendanceDate.Month,
                Year = x.attendanceDate.Year,
            })
            .OrderByDescending(x => x.Year).ThenByDescending(y => y.MonthId).ToList();
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
                TotalMembersPresent = (int)x.totalMembersPresent,
                TotalMembersAbsent = (int)x.totalMembersAbsent,
                TotalDuesPaid = (decimal)x.totalDuesPaid,
                FormattedTotalDuesPaid = ((decimal)x.totalDuesPaid).ToString(),
                TotalDuesExpected = (decimal)x.totalDuesExpected,
                FormattedTotalDuesExpected = ((decimal)x.totalDuesExpected).ToString(),
                TotalDuesBalance = ((decimal)x.totalDuesExpected - (decimal)x.totalDuesPaid).ToString(),
                FinesRaised = _finedMemberRepository.GetAllByDate(x.attendanceDate).Sum(y => y.amountPaid).ToString(),
                Summary = x.summary,
                MonthName = GeneralHelper.ConventIntToMonth(x.attendanceDate.Month),
                MonthId = x.attendanceDate.Month,
                Year = x.attendanceDate.Year,
            })
            .OrderByDescending(x => x.Year).ThenByDescending(y => y.MonthId).ToList();
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
