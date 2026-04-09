using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;


namespace APC.Applications.Services
{
    public class MemberCommittmentService : IMemberCommittmentService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGeneralMeetingAttendanceRepository _meetingAttendanceRepository;
        private readonly IFinedMemberRepository _finedMemberRepository;
        public MemberCommittmentService(IMemberRepository memberRepository, IGeneralMeetingAttendanceRepository meetingAttendanceRepository,
            IFinedMemberRepository finedMemberRepository)
        {
            _memberRepository = memberRepository;
            _meetingAttendanceRepository = meetingAttendanceRepository;
            _finedMemberRepository = finedMemberRepository;
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
                var attendance = _db.PERSONAL_ATTENDANCE
                    .Where(x => !x.isDeleted
                        && x.memberID == member.memberID
                        && x.year == year)
                    .ToList();

                decimal contributed = attendance.Sum(x => (decimal?)x.monthlyDues ?? 0);

                int present = attendance.Count(x => x.attendanceStatusID == 2);
                int absent = attendance.Count(x => x.attendanceStatusID == 3);

                decimal expected = GeneralHelper.CalculateYearlyDue(member.membershipDate.Value, year);

                string balance;
                if (expected > contributed)
                    balance = (expected - contributed) + " € Remaining";
                else if (expected == contributed)
                    balance = "Completed";
                else
                    balance = (contributed - expected) + " € Extra";

                var fines = (
                    from f in _db.FINED_MEMBER
                    join c in _db.CONSTITUTION on f.constitutionID equals c.constitutionID
                    where !f.isdeleted
                        && !c.isDeleted
                        && f.memberID == member.memberID
                        && f.year == year
                    select c.fine
                ).ToList();

                decimal totalFines = fines.Sum();

                decimal paidFines = _db.FINED_MEMBER
                    .Where(x => !x.isdeleted
                        && x.memberID == member.memberID
                        && x.year == year)
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

                var duesRatioSum = _db.PERSONAL_ATTENDANCE
                    .Where(x => !x.isDeleted
                        && x.memberID == member.memberID
                        && x.year == year
                        && x.monthID <= endMonth)
                    .Sum(x => (decimal?)x.monthlyDues) ?? 0;

                decimal duesRatio =
                    duesRatioSum > 120
                        ? 50 + (((duesRatioSum - 120) / 120) * 0.5m)
                        : (duesRatioSum / 120) * 50;

                int attendanceRatioCount = _db.PERSONAL_ATTENDANCE
                    .Count(x => !x.isDeleted
                        && x.memberID == member.memberID
                        && x.year == year
                        && x.monthID <= endMonth + 1
                        && x.attendanceStatusID == 2);

                decimal attendanceRatio =
                    ((decimal)attendanceRatioCount / (endMonth + 1)) * 40;

                var behaviorRecords = _db.FINED_MEMBER
                    .Where(x => !x.isdeleted
                        && x.memberID == member.memberID
                        && x.year == year
                        && x.monthID <= endMonth + 1)
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
                    ContributedDues = contributed,
                    BalanceDues = expected - contributed,

                    TotalFines = totalFines,
                    PaidFines = paidFines,

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
    }
}
