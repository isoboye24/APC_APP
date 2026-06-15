using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace APC.Applications.Services
{
    public class MemberCommittmentService : IMemberCommittmentService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGeneralMeetingAttendanceRepository _generalMeetingAttendanceRepository;
        private readonly IGeneralMeetingRepository _generalMeetingRepository;
        private readonly IFinedMemberRepository _finedMemberRepository;
        private readonly IConstitutionRepository _constitutionRepository;
        public MemberCommittmentService(IMemberRepository memberRepository, IGeneralMeetingAttendanceRepository generalMeetingAttendanceRepository,
            IFinedMemberRepository finedMemberRepository, IConstitutionRepository constitutionRepository, IGeneralMeetingRepository generalMeetingRepository)
        {
            _memberRepository = memberRepository;
            _generalMeetingAttendanceRepository = generalMeetingAttendanceRepository;
            _finedMemberRepository = finedMemberRepository;
            _constitutionRepository = constitutionRepository;
            _generalMeetingRepository = generalMeetingRepository;
        }

        private AttendanceStatisticsDTO GetMemberAttendanceStatistics(int memberId,int year)
        {
            var records =
                from attendance in _generalMeetingAttendanceRepository.GetAll()
                join meeting in _generalMeetingRepository.GetAll()
                    on attendance.generalAttendanceID equals meeting.generalAttendanceID
                where meeting.attendanceDate.Year == year
                      && attendance.memberID == memberId
                select attendance;

            return new AttendanceStatisticsDTO
            {
                TotalContribution = records.Sum(x => (decimal?)x.monthlyDues) ?? 0,
                PresentCount = records.Count(x => x.attendanceStatusID == 2),
                AbsentCount = records.Count(x => x.attendanceStatusID == 3)
            };
        }

        public List<MemberCommittmentDTO> GetMembersCommittment(int year)
        {
            int endMonth = 10;

            var members = _memberRepository.GetAll().Where(x => x.membershipStatusID == 1 && x.membershipDate.Value.Year <= year)
                .Select(m => new
                {
                    m.memberID,
                    m.name,
                    m.surname,
                    m.imagePath,
                    m.membershipDate
                })
                .ToList();

            var result = new List<MemberCommittmentDTO>();



            foreach (var member in members)
            {
                var attendance = GetMemberAttendanceStatistics(member.memberID, year);

                decimal contributed = attendance.TotalContribution;

                int present = attendance.PresentCount;
                int absent = attendance.AbsentCount;

                decimal expected = GeneralHelper.CalculateYearlyDue(member.membershipDate.Value, year);

                string balance;
                if (expected > contributed)
                    balance = (expected - contributed) + " € Remaining";
                else if (expected == contributed)
                    balance = "Completed";
                else
                    balance = (contributed - expected) + " € Extra";

                var fines = (
                    from f in _finedMemberRepository.GetAll()
                    join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                    where !f.isdeleted
                        && !c.isDeleted
                        && f.memberID == member.memberID
                        && f.fineDate.Year == year
                    select c.fine
                ).ToList();

                decimal totalFines = fines.Sum();

                decimal paidFines = _finedMemberRepository.GetAll()
                    .Where(x => !x.isdeleted
                        && x.memberID == member.memberID
                        && x.fineDate.Year == year)
                    .Sum(x => (decimal?)x.amountPaid) ?? 0;

                string paymentStatus;

                if (contributed == 0 && paidFines == 0)
                    paymentStatus = "Not Paid";
                else if (contributed == expected && paidFines == totalFines)
                    paymentStatus = "Completed";
                else if (contributed > expected || paidFines > totalFines)
                    paymentStatus = "Extra";
                else
                    paymentStatus = "Incomplete";

                //---------------------------------------
                // Rank calculation
                //---------------------------------------

                var duesRatioSum = (
                    from attend in _generalMeetingAttendanceRepository.GetAll()
                    join meeting in _generalMeetingRepository.GetAll()
                    on attend.generalAttendanceID equals meeting.generalAttendanceID
                    where meeting.attendanceDate.Year == year 
                    && meeting.attendanceDate.Month <= endMonth
                    && attend.memberID == member.memberID
                    select attend.monthlyDues
                    ).Sum(x => (decimal?)x) ?? 0;

                decimal duesRatio =
                    duesRatioSum > 120
                        ? 50 + (((duesRatioSum - 120) / 120) * 0.5m)
                        : (duesRatioSum / 120) * 50;

                int attendanceRatioCount = (
                    from attend in _generalMeetingAttendanceRepository.GetAll()
                    join meeting in _generalMeetingRepository.GetAll()
                    on attend.generalAttendanceID equals meeting.generalAttendanceID
                    where meeting.attendanceDate.Year == year
                    && meeting.attendanceDate.Month <= endMonth + 1
                    && attend.memberID == member.memberID
                    && attend.attendanceStatusID == 2
                    select attend.attendanceStatusID
                    ).Count();

                decimal attendanceRatio =
                    ((decimal)attendanceRatioCount / (endMonth + 1)) * 40;

                var behaviorRecords = _finedMemberRepository.GetAll()
                    .Where(x => !x.isdeleted
                        && x.memberID == member.memberID
                        && x.fineDate.Year == year
                        && x.fineDate.Month <= endMonth + 1)
                    .ToList();

                decimal behaviorPenalty = 0;

                foreach (var b in behaviorRecords)
                {
                    if (b.amountPaid == null)
                        behaviorPenalty += 1.2m;
                    else if (b.amountPaid <= 2)
                        behaviorPenalty += 0.05m;
                    else if (b.amountPaid <= 5)
                        behaviorPenalty += 0.2m;
                    else if (b.amountPaid <= 20)
                        behaviorPenalty += 0.5m;
                    else
                        behaviorPenalty += 1m;
                }

                decimal behaviorRatio = 10 - behaviorPenalty;

                decimal rank = duesRatio + attendanceRatio + behaviorRatio;

                result.Add(new MemberCommittmentDTO
                {
                    MemberId = member.memberID,
                    FirstName = member.name,
                    LastName = member.surname,
                    ImagePath = member.imagePath,

                    ExpectedDues = expected,
                    FormattedExpectedDues = expected.ToString() + " €",

                    ContributedDues = contributed,
                    FormattedContributedDues = contributed.ToString() + " €",

                    BalanceDues = expected - contributed,
                    FormattedBalanceDues = (expected - contributed).ToString() + " €",

                    TotalFines = totalFines,
                    FormattedTotalFines = totalFines.ToString() + " €",

                    PaidFines = paidFines,
                    FormattedPaidFines = paidFines.ToString() + " €",

                    NoOfPresent = present,
                    NoOfAbsent = absent,

                    Status = paymentStatus,
                    Rank = rank,
                    ShowRank = Math.Round(rank, 2)
                });
            }

            return result
                .OrderByDescending(x => x.Rank)
                .ToList();
        }

        public List<YearDTO> GetMeetingYears()
        {
            return (
                from attendance in _generalMeetingAttendanceRepository.GetAll()
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
