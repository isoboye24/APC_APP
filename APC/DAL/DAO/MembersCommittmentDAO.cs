using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class MembersCommittmentDAO :APCContexts
    {
        public List<MembersCommittmentDetailDTO> Select(int year)
        {
            try
            {
                List<MembersCommittmentDetailDTO> membersCommittment = new List<MembersCommittmentDetailDTO>();

                var memberList = db.MEMBER.Where(x => x.isDeleted == false && x.membershipStatusID == 1 && x.membershipDate.Value.Year <= year).ToList();
                foreach (var member in memberList)
                {
                    MembersCommittmentDetailDTO dto = new MembersCommittmentDetailDTO();
                    dto.MemberID = member.memberID;
                    dto.Name = member.name;
                    dto.Surname = member.surname;
                    dto.ImagePath = member.imagePath;                    

                    List<decimal> amountContributed = new List<decimal>();
                    List<int> NumberOfPresence = new List<int>();
                    List<int> NumberOfAbsence = new List<int>();

                    var attendanceList = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.memberID == member.memberID && x.year == year).ToList();
                    foreach (var attendance in attendanceList)
                    {
                        amountContributed.Add((decimal)attendance.monthlyDues);
                        int attendanceStatus = attendance.attendanceStatusID;
                        if (attendanceStatus == 2)
                        {
                            NumberOfPresence.Add(1);
                            NumberOfAbsence.Add(0);
                        }
                        else if (attendanceStatus == 3)
                        {
                            NumberOfAbsence.Add(1);
                            NumberOfPresence.Add(0);
                        }
                        else
                        {
                            NumberOfAbsence.Add(0);
                            NumberOfPresence.Add(0);
                        }
                    }
                    dto.Contributed = amountContributed.Sum();
                    dto.NumberOfPresence = NumberOfPresence.Sum();
                    dto.NumberOfAbsence = NumberOfAbsence.Sum();
                    
                    dto.ExpectedAmount = GeneralHelper.CalculateYearlyDue((DateTime)member.membershipDate, year);

                    if (120 > amountContributed.Sum())
                    {
                        dto.Balance = (dto.ExpectedAmount - amountContributed.Sum()) + " € Remaining";
                    }
                    else if ((dto.ExpectedAmount - amountContributed.Sum()) == 0)
                    {
                        dto.Balance = "Completed";
                    }
                    else
                    {
                        dto.Balance = (amountContributed.Sum() - dto.ExpectedAmount) + " € Extra";
                    }

                    List<decimal> totalFines = new List<decimal>();
                    var finesList = (from f in db.FINED_MEMBER.Where(x => x.isdeleted == false && x.memberID == member.memberID && x.year == year)
                                     join c in db.CONSTITUTION.Where(x => x.isDeleted == false) on f.constitutionID equals c.constitutionID
                                     select new
                                     {
                                         fine = c.fine,
                                     }).ToList();
                    foreach (var fine in finesList)
                    {
                        totalFines.Add(fine.fine);
                    }
                    dto.Fines = totalFines.Sum();

                    List<decimal> paidFines = new List<decimal>();
                    var finePaidList = db.FINED_MEMBER.Where(x => x.isdeleted == false && x.memberID == member.memberID && x.year == year).ToList();
                    foreach (var paidFine in finePaidList)
                    {
                        paidFines.Add((decimal)paidFine.amountPaid);
                    }
                    dto.PaidFines = paidFines.Sum();

                    // -------------------------------------------------------------------------
                    // -------------------- RANK RATIO CALCULATION ----------------------
                    // -------------------------------------------------------------------------

                    int endMonth = 10;
                    List<decimal> amountContributedRatio = new List<decimal>();
                    List<int> numberOfPresenceRatio = new List<int>();
                    List<decimal> bahaviorRatioList = new List<decimal>();

                    var duesRatioList = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.memberID == member.memberID && x.year == year && x.monthID <= endMonth).ToList();
                    foreach (var due in duesRatioList)
                    {
                        amountContributedRatio.Add((decimal)due.monthlyDues);
                    }
                    decimal duesRatio = amountContributedRatio.Sum();

                    if (duesRatio > 120)
                    {
                        duesRatio = 50 + (((duesRatio - 120) / 120) * 0.5m);
                    }
                    else
                    {
                        duesRatio = (duesRatio / 120) * 50;
                    }

                    var attendanceRatioList = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.memberID == member.memberID && x.year == year && x.monthID <= (endMonth + 1)).ToList();
                    foreach (var attendanceRatio in attendanceRatioList)
                    {                        
                        int attendanceStatus = attendanceRatio.attendanceStatusID;
                        if (attendanceStatus == 2)
                        {
                            numberOfPresenceRatio.Add(1);
                        }
                    }
                    decimal totalAttendSum = numberOfPresenceRatio.Sum();
                    decimal AttendanceRatio = (totalAttendSum / (endMonth + 1)) * 40;

                    int behaviorCount = db.FINED_MEMBER.Count(x => x.isdeleted == false && x.memberID == member.memberID && x.year == year && x.monthID <= (endMonth + 1));
                    var behaviorList = db.FINED_MEMBER.Where(x => x.isdeleted == false && x.memberID == member.memberID && x.year == year && x.monthID <= (endMonth + 1)).ToList();
                    if (behaviorCount == 0)
                    {
                        bahaviorRatioList.Add(0m);
                    }
                    else
                    {
                        foreach (var behavior in behaviorList)
                        {
                            if (behavior.amountPaid == null)
                            {
                                bahaviorRatioList.Add(1.2m);
                            }
                            else if (behavior.amountPaid > 0 && behavior.amountPaid <= 2)
                            {
                                bahaviorRatioList.Add(0.05m);
                            }
                            else if (behavior.amountPaid > 2 && behavior.amountPaid <= 5)
                            {
                                bahaviorRatioList.Add(0.2m);
                            }
                            else if (behavior.amountPaid > 5 && behavior.amountPaid <= 20)
                            {
                                bahaviorRatioList.Add(0.5m);
                            }
                            else
                            {
                                bahaviorRatioList.Add(1.0m);
                            }
                        }
                    }

                    decimal totalBehaviour = bahaviorRatioList.Sum();
                    decimal totalBehaviorRatio = 10 - totalBehaviour;

                    dto.Rank = (duesRatio + AttendanceRatio + totalBehaviorRatio);
                    dto.ShowRank = Math.Round(duesRatio + AttendanceRatio + totalBehaviorRatio, 2);


                    membersCommittment.Add(dto);
                }

                var orderedList = membersCommittment
                                .OrderByDescending(x => x.Rank).ToList();

                return orderedList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
