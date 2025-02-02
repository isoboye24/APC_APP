using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class GeneralAttendanceDAO : APCContexts, IDAO<GeneralAttendanceDetailDTO, GENERAL_ATTENDANCE>
    {
        public bool Delete(GENERAL_ATTENDANCE entity)
        {
            try
            {
                GENERAL_ATTENDANCE generalAttendance = db.GENERAL_ATTENDANCE.First(x=>x.generalAttendanceID == entity.generalAttendanceID);
                generalAttendance.isDeleted = true;
                generalAttendance.deletedDate = DateTime.Today;
                var list = db.PERSONAL_ATTENDANCE.Where(x=>x.generalAttendanceID==entity.generalAttendanceID).ToList();
                foreach (var item in list)
                {
                    item.isDeleted = true;
                    item.deletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool GetBack(int ID)
        {
            try
            {
                GENERAL_ATTENDANCE genAttendance = db.GENERAL_ATTENDANCE.First(x=>x.generalAttendanceID==ID);
                genAttendance.isDeleted = false;
                genAttendance.deletedDate = null;
                var list = db.PERSONAL_ATTENDANCE.Where(x => x.generalAttendanceID == ID).ToList();
                foreach (var item in list)
                {
                    item.isDeleted = false;
                    item.deletedDate = null;
                    db.SaveChanges();
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(GENERAL_ATTENDANCE entity)
        {
            try
            {
                db.GENERAL_ATTENDANCE.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GeneralAttendanceDetailDTO> Select()
        {
            try
            { 
                List<GeneralAttendanceDetailDTO> MeetingReports = new List<GeneralAttendanceDetailDTO>();
                List<int> monthIDCollection = new List<int>();
                List<int> monthIDs = new List<int>();
                List<int> yearsCollection = new List<int>();
                List<int> years = new List<int>();
                List<decimal> totalDuesCollection = new List<decimal>();
                List<decimal> totalExpectedDues = new List<decimal>();

                var yearlyDue = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).ToList();
                foreach (var item in yearlyDue)
                {
                    yearsCollection.Add(item.year);
                }
                years = yearsCollection.Distinct().OrderByDescending(year => year).ToList();
                foreach (var yearItem in years)
                {
                    var monthlyDues = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false && x.year == yearItem).ToList();
                    foreach (var item in monthlyDues)
                    {
                        monthIDCollection.Add(item.monthID);
                    }
                    monthIDs = monthIDCollection.Distinct().OrderByDescending(monthID => monthID).ToList();
                    monthIDCollection.Clear();
                    foreach (var monthItem in monthIDs)
                    {
                        GeneralAttendanceDetailDTO dto = new GeneralAttendanceDetailDTO();
                        var monthlyDue = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.monthID == monthItem).ToList();
                        foreach (var due in monthlyDue)
                        {
                            totalDuesCollection.Add((decimal)due.monthlyDues);
                            totalExpectedDues.Add((decimal)due.expectedMonthlyDue);
                        }
                        var meeting = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false && x.monthID == monthItem && x.year == yearItem).FirstOrDefault();
                        dto.GeneralAttendanceID = meeting.generalAttendanceID;
                        dto.Day = meeting.day;
                        dto.Summary = meeting.summary;
                        dto.AttendanceDate = meeting.attendanceDate;
                        dto.MonthID = monthItem;
                        dto.Year = yearItem.ToString();
                        dto.TotalDuesPaid = totalDuesCollection.Sum();                
                        dto.TotalDuesExpected = totalExpectedDues.Sum();
                        dto.TotalDuesBalance = dto.TotalDuesExpected - dto.TotalDuesPaid;
                        dto.Month = General.ConventIntToMonth(monthItem);
                        dto.TotalMembersPresent = db.PERSONAL_ATTENDANCE.Count(x => x.isDeleted == false && x.year == yearItem && x.monthID == monthItem && x.attendanceStatusID == 2);
                        dto.TotalMembersAbsent = db.PERSONAL_ATTENDANCE.Count(x => x.isDeleted == false && x.year == yearItem && x.monthID == monthItem && x.attendanceStatusID == 3);
                        MeetingReports.Add(dto);
                        totalDuesCollection.Clear();
                        totalExpectedDues.Clear();
                    }
                }
                return MeetingReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GeneralAttendanceDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<GeneralAttendanceDetailDTO> generalAttendance = new List<GeneralAttendanceDetailDTO>();
                var list = (from g in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == isDeleted)
                            join m in db.MONTHs on g.monthID equals m.monthID
                            select new
                            {
                                generalAttendanceID = g.generalAttendanceID,
                                day = g.day,
                                monthID = g.monthID,
                                monthName = m.monthName,
                                year = g.year,
                                totalMembersPresent = g.totalMembersPresent,
                                totalMembersAbsent = g.totalMembersAbsent,
                                totalDuesPaid = g.totalDuesPaid,
                                totalDuesExpected = g.totalDuesExpected,
                                totalDuesBalance = g.totalDuesBalance,
                                summary = g.summary,
                                attendanceDate = g.attendanceDate,
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ToList();
                foreach (var item in list)
                {
                    GeneralAttendanceDetailDTO dto = new GeneralAttendanceDetailDTO();
                    dto.GeneralAttendanceID = item.generalAttendanceID;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.Month = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.TotalMembersPresent = (int)item.totalMembersPresent;
                    dto.TotalMembersAbsent = (int)item.totalMembersAbsent;
                    dto.TotalDuesPaid = (decimal)item.totalDuesPaid;
                    dto.TotalDuesExpected = (decimal)item.totalDuesExpected;
                    dto.TotalDuesBalance = (decimal)item.totalDuesBalance;
                    dto.Summary = item.summary;
                    dto.AttendanceDate = item.attendanceDate;
                    generalAttendance.Add(dto);
                }
                return generalAttendance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal SelectMonthlyDues(int month)
        {
            try
            {
                List<decimal> monthlyDues = new List<decimal>();
                var list = (from g in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false)
                            join m in db.MONTHs.Where(x=>x.monthID == month) on g.monthID equals m.monthID
                            select new
                            {
                                generalAttendanceID = g.generalAttendanceID,                               
                                totalDuesPaid = g.totalDuesPaid,                                
                            }).ToList();
                foreach (var item in list)
                {
                    GeneralAttendanceDetailDTO dto = new GeneralAttendanceDetailDTO();
                    dto.GeneralAttendanceID = item.generalAttendanceID;
                    monthlyDues.Add((decimal)item.totalDuesPaid);                    
                }
                decimal totalMonthlyDues = monthlyDues.Sum();
                return totalMonthlyDues;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckMeeting(int day, int month, int year)
        {
            try
            {
                int meetingCount = db.GENERAL_ATTENDANCE.Count(x=>x.isDeleted == false && x.day == day && x.monthID == month && x.year == year);
                if (meetingCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal SelectYearlyDues(int year)
        {
            try
            {
                List<decimal> yearlyDues = new List<decimal>();
                var list = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false && x.year == year);          
                foreach (var item in list)
                {
                    GeneralAttendanceDetailDTO dto = new GeneralAttendanceDetailDTO();
                    dto.GeneralAttendanceID = item.generalAttendanceID;
                    yearlyDues.Add((decimal)item.totalDuesPaid);
                }
                decimal totalyearlyDues = yearlyDues.Sum();
                return totalyearlyDues;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(GENERAL_ATTENDANCE entity)
        {
            try
            {
                GENERAL_ATTENDANCE generalAttendance = db.GENERAL_ATTENDANCE.First(x=>x.generalAttendanceID == entity.generalAttendanceID);
                generalAttendance.summary = entity.summary;
                generalAttendance.day = entity.day;
                generalAttendance.monthID = entity.monthID;
                generalAttendance.year = entity.year;
                generalAttendance.totalMembersPresent = entity.totalMembersPresent;
                generalAttendance.totalMembersAbsent = entity.totalMembersAbsent;
                generalAttendance.totalDuesPaid = entity.totalDuesPaid;
                generalAttendance.totalDuesExpected = entity.totalDuesExpected;
                generalAttendance.totalDuesBalance = entity.totalDuesBalance;
                generalAttendance.attendanceDate = entity.attendanceDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getSinglePartsOfSpeechQuery(int year)
        {
            string singlePartsOfSpeechQuery = "SELECT MONTH.monthID, COUNT(WORD) \r\n" +
            "FROM WORD \r\n" +
            "JOIN PARTS_OF_SPEECH ON WORD.partOfSpeechID = PARTS_OF_SPEECH.partOfSpeechID \r\n" +
            "JOIN MONTH ON WORD.monthID = MONTH.monthID \r\n" +
            "WHERE WORD.year = @year AND WORD.isDeleted = 0 AND PARTS_OF_SPEECH.partsOfSpeechName = @partOfSpeech \r\n" +
            "GROUP BY MONTH.monthID\r\n" +
            "ORDER BY MONTH.monthID ASC";
            return singlePartsOfSpeechQuery.Replace("@year", year.ToString());
        }
    }
}
