using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    internal class PersonalAttendanceDAO : APCContexts, IDAO<PersonalAttendanceDetailDTO, PERSONAL_ATTENDANCE>
    {
        public bool Delete(PERSONAL_ATTENDANCE entity)
        {
            try
            {
                if (entity.generalAttendanceID != 0)
                {
                    List<PERSONAL_ATTENDANCE> personalAttendance = db.PERSONAL_ATTENDANCE.Where(x => x.generalAttendanceID == entity.generalAttendanceID).ToList();
                    foreach (var item in personalAttendance)
                    {
                        item.isDeleted = true;
                        item.deletedDate = DateTime.Today;
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PERSONAL_ATTENDANCE entity)
        {
            try
            {
                db.PERSONAL_ATTENDANCE.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalAttendanceDetailDTO> Select()
        {
            List<PersonalAttendanceDetailDTO> attendances = new List<PersonalAttendanceDetailDTO>();
            return attendances;
        }
        public List<int> SelectOnlyYears()
        {
            try
            {
                List<int> years = new List<int>();
                List<int> AllYears = new List<int>();
                var list = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ToList();
                foreach (var item in list)
                {
                    AllYears.Add(item.year);
                }
                years = AllYears.Distinct().ToList();
                return years;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalAttendanceDetailDTO> SelectPresentMember(int ID)
        {
            try
            {
                List<PersonalAttendanceDetailDTO> attendances = new List<PersonalAttendanceDetailDTO>();
                List<int> members = new List<int>();
                List<int> absentMembers = new List<int>();
                var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false)
                            join mem in db.MEMBER.Where(x => x.isDeleted == false && x.memberID == ID) on p.memberID equals mem.memberID
                            join m in db.MONTH on p.monthID equals m.monthID
                            join g in db.GENDER on mem.genderID equals g.genderID
                            join ats in db.ATTENDANCE_STATUS.Where(x => x.attendanceStatus == "Present") on p.attendanceStatusID equals ats.attendanceStatusID
                            join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID) on p.generalAttendanceID equals gen.generalAttendanceID
                            select new
                            {
                                attendanceID = p.attendanceID,
                                attendanceStatusID = p.attendanceStatusID,
                                attendanceStatus = ats.attendanceStatus,
                                day = p.day,
                                monthID = p.monthID,
                                monthName = m.monthName,
                                year = p.year,
                                memberID = p.memberID,
                                surname = mem.surname,
                                name = mem.name,
                                imagePath = mem.imagePath,
                                genderID = mem.genderID,
                                gender = g.genderName,
                                monthlyDues = p.monthlyDues,
                                expectedDue = p.expectedMonthlyDue,
                                balance = p.balance,
                                meetingID = p.generalAttendanceID,
                            }).ToList();
                foreach (var item in list)
                {
                    PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                    dto.AttendanceID = item.attendanceID;
                    dto.AttendanceStatusID = item.attendanceStatusID;
                    dto.Day = (int)item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.MemberID = item.memberID;
                    dto.Surname = item.surname;
                    dto.Name = item.name;
                    dto.ImagePath = item.imagePath;
                    dto.GenderID = item.genderID;
                    dto.Gender = item.gender;
                    dto.AttendanceStatusName = item.attendanceStatus;
                    dto.MonthlyDue = (decimal)item.monthlyDues;
                    dto.ExpectedDue = (decimal)item.expectedDue;
                    dto.Balance = (decimal)item.balance;
                    dto.GeneralAttendanceID = item.meetingID;
                    attendances.Add(dto);
                }
                return attendances;
            }
            catch (Exception ex) {
                throw ex;
            }            
        }

        public List<PersonalAttendanceDetailDTO> SelectAbsentMember(int ID)
        {
            List<PersonalAttendanceDetailDTO> attendances = new List<PersonalAttendanceDetailDTO>();
            List<int> members = new List<int>();
            List<int> absentMembers = new List<int>();
            var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false)
                        join mem in db.MEMBER.Where(x => x.isDeleted == false && x.memberID == ID) on p.memberID equals mem.memberID
                        join m in db.MONTH on p.monthID equals m.monthID
                        join g in db.GENDER on mem.genderID equals g.genderID
                        join ats in db.ATTENDANCE_STATUS.Where(x => x.attendanceStatus == "Absent") on p.attendanceStatusID equals ats.attendanceStatusID
                        join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID) on p.generalAttendanceID equals gen.generalAttendanceID
                        select new
                        {
                            attendanceID = p.attendanceID,
                            attendanceStatusID = p.attendanceStatusID,
                            attendanceStatus = ats.attendanceStatus,
                            day = p.day,
                            monthID = p.monthID,
                            monthName = m.monthName,
                            year = p.year,
                            memberID = p.memberID,
                            surname = mem.surname,
                            name = mem.name,
                            imagePath = mem.imagePath,
                            genderID = mem.genderID,
                            gender = g.genderName,
                            monthlyDues = p.monthlyDues,
                            expectedDue = p.expectedMonthlyDue,
                            balance = p.balance,
                            meetingID = p.generalAttendanceID,
                        }).ToList();
            foreach (var item in list)
            {
                PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                dto.AttendanceID = item.attendanceID;
                dto.AttendanceStatusID = item.attendanceStatusID;
                dto.Day = (int)item.day;
                dto.MonthID = item.monthID;
                dto.MonthName = item.monthName;
                dto.Year = item.year.ToString();
                dto.MemberID = item.memberID;
                dto.Surname = item.surname;
                dto.Name = item.name;
                dto.ImagePath = item.imagePath;
                dto.GenderID = item.genderID;
                dto.Gender = item.gender;
                dto.AttendanceStatusName = item.attendanceStatus;
                dto.MonthlyDue = (decimal)item.monthlyDues;
                dto.ExpectedDue = (decimal)item.expectedDue;
                dto.Balance = (decimal)item.balance;
                dto.GeneralAttendanceID = item.meetingID;
                attendances.Add(dto);
            }
            return attendances;
        }

        public List<PersonalAttendanceDetailDTO> AmountsContributed(int ID)
        {
            List<PersonalAttendanceDetailDTO> amountsContributed = new List<PersonalAttendanceDetailDTO>();
            List<int> members = new List<int>();
            List<int> absentMembers = new List<int>();
            var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.monthlyDues > 0)
                        join mem in db.MEMBER.Where(x => x.isDeleted == false && x.memberID == ID) on p.memberID equals mem.memberID
                        join m in db.MONTH on p.monthID equals m.monthID
                        join g in db.GENDER on mem.genderID equals g.genderID
                        join ats in db.ATTENDANCE_STATUS on p.attendanceStatusID equals ats.attendanceStatusID
                        join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID) on p.generalAttendanceID equals gen.generalAttendanceID
                        select new
                        {
                            attendanceID = p.attendanceID,
                            attendanceStatusID = p.attendanceStatusID,
                            attendanceStatus = ats.attendanceStatus,
                            day = p.day,
                            monthID = p.monthID,
                            monthName = m.monthName,
                            year = p.year,
                            memberID = p.memberID,
                            surname = mem.surname,
                            name = mem.name,
                            imagePath = mem.imagePath,
                            genderID = mem.genderID,
                            gender = g.genderName,
                            monthlyDues = p.monthlyDues,
                            expectedDue = p.expectedMonthlyDue,
                            balance = p.balance,
                            meetingID = p.generalAttendanceID,
                        }).ToList();
            foreach (var item in list)
            {
                PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                dto.AttendanceID = item.attendanceID;
                dto.AttendanceStatusID = item.attendanceStatusID;
                dto.Day = (int)item.day;
                dto.MonthID = item.monthID;
                dto.MonthName = item.monthName;
                dto.Year = item.year.ToString();
                dto.MemberID = item.memberID;
                dto.Surname = item.surname;
                dto.Name = item.name;
                dto.ImagePath = item.imagePath;
                dto.GenderID = item.genderID;
                dto.Gender = item.gender;
                dto.AttendanceStatusName = item.attendanceStatus;
                dto.MonthlyDue = (decimal)item.monthlyDues;
                dto.ExpectedDue = (decimal)item.expectedDue;
                dto.Balance = (decimal)item.balance;
                dto.GeneralAttendanceID = item.meetingID;
                amountsContributed.Add(dto);
            }
            return amountsContributed;
        }

        public decimal AmountExpected(int meetingID)
        {
            try
            {
                decimal amountExpected  = db.PERSONAL_ATTENDANCE.Count(x =>x.generalAttendanceID == meetingID) * 10;
                return amountExpected;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public decimal DuesContributed(int meetingID)
        {
            try
            {
                List<decimal> duesContributed = new List<decimal>();
                var totalDues  = db.PERSONAL_ATTENDANCE.Where(x =>x.generalAttendanceID == meetingID).ToList();

                foreach (var item in totalDues)
                {
                    duesContributed.Add((decimal)item.monthlyDues);
                }
                decimal totalDuesContributed = duesContributed.Sum();

                return totalDuesContributed;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalAttendanceDetailDTO> AmountsExpected(int ID)
        {
            List<PersonalAttendanceDetailDTO> amountsExpected = new List<PersonalAttendanceDetailDTO>();
            List<int> members = new List<int>();
            List<int> absentMembers = new List<int>();
            var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false)
                        join mem in db.MEMBER.Where(x => x.isDeleted == false && x.memberID == ID) on p.memberID equals mem.memberID
                        join m in db.MONTH on p.monthID equals m.monthID
                        join g in db.GENDER on mem.genderID equals g.genderID
                        join ats in db.ATTENDANCE_STATUS.Where(x=>x.attendanceStatus == "Present" || x.attendanceStatus == "Absent") on p.attendanceStatusID equals ats.attendanceStatusID
                        join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID) on p.generalAttendanceID equals gen.generalAttendanceID
                        select new
                        {
                            attendanceID = p.attendanceID,
                            attendanceStatusID = p.attendanceStatusID,
                            attendanceStatus = ats.attendanceStatus,
                            day = p.day,
                            monthID = p.monthID,
                            monthName = m.monthName,
                            year = p.year,
                            memberID = p.memberID,
                            surname = mem.surname,
                            name = mem.name,
                            imagePath = mem.imagePath,
                            genderID = mem.genderID,
                            gender = g.genderName,
                            monthlyDues = p.monthlyDues,
                            expectedDue = p.expectedMonthlyDue,
                            balance = p.balance,
                            meetingID = p.generalAttendanceID,
                        }).ToList();
            foreach (var item in list)
            {
                PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                dto.AttendanceID = item.attendanceID;
                dto.AttendanceStatusID = item.attendanceStatusID;
                dto.Day = (int)item.day;
                dto.MonthID = item.monthID;
                dto.MonthName = item.monthName;
                dto.Year = item.year.ToString();
                dto.MemberID = item.memberID;
                dto.Surname = item.surname;
                dto.Name = item.name;
                dto.ImagePath = item.imagePath;
                dto.GenderID = item.genderID;
                dto.Gender = item.gender;
                dto.AttendanceStatusName = item.attendanceStatus;
                dto.MonthlyDue = (decimal)item.monthlyDues;
                dto.ExpectedDue = (decimal)item.expectedDue;
                dto.Balance = (decimal)item.balance;
                dto.GeneralAttendanceID = item.meetingID;
                amountsExpected.Add(dto);
            }
            return amountsExpected;
        }
        public List<PersonalAttendanceDetailDTO> AmountsBalance(int ID)
        {
            List<PersonalAttendanceDetailDTO> amountsBalance = new List<PersonalAttendanceDetailDTO>();
            List<int> members = new List<int>();
            List<int> absentMembers = new List<int>();
            var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.monthlyDues < x.expectedMonthlyDue)
                        join mem in db.MEMBER.Where(x => x.isDeleted == false && x.memberID == ID) on p.memberID equals mem.memberID
                        join m in db.MONTH on p.monthID equals m.monthID
                        join g in db.GENDER on mem.genderID equals g.genderID
                        join ats in db.ATTENDANCE_STATUS.Where(x => x.attendanceStatus == "Present" || x.attendanceStatus == "Absent") on p.attendanceStatusID equals ats.attendanceStatusID
                        join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x=>x.monthID) on p.generalAttendanceID equals gen.generalAttendanceID
                        select new
                        {
                            attendanceID = p.attendanceID,
                            attendanceStatusID = p.attendanceStatusID,
                            attendanceStatus = ats.attendanceStatus,
                            day = p.day,
                            monthID = p.monthID,
                            monthName = m.monthName,
                            year = p.year,
                            memberID = p.memberID,
                            surname = mem.surname,
                            name = mem.name,
                            imagePath = mem.imagePath,
                            genderID = mem.genderID,
                            gender = g.genderName,
                            monthlyDues = p.monthlyDues,
                            expectedDue = p.expectedMonthlyDue,
                            balance = p.balance,
                            meetingID = p.generalAttendanceID,
                        }).ToList();
            foreach (var item in list)
            {
                PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                dto.AttendanceID = item.attendanceID;
                dto.AttendanceStatusID = item.attendanceStatusID;
                dto.Day = (int)item.day;
                dto.MonthID = item.monthID;
                dto.MonthName = item.monthName;
                dto.Year = item.year.ToString();
                dto.MemberID = item.memberID;
                dto.Surname = item.surname;
                dto.Name = item.name;
                dto.ImagePath = item.imagePath;
                dto.GenderID = item.genderID;
                dto.Gender = item.gender;
                dto.AttendanceStatusName = item.attendanceStatus;
                dto.MonthlyDue = (decimal)item.monthlyDues;
                dto.ExpectedDue = (decimal)item.expectedDue;
                dto.Balance = (decimal)item.balance;
                dto.GeneralAttendanceID = item.meetingID;
                amountsBalance.Add(dto);
            }
            return amountsBalance;
        }
        public List<PersonalAttendanceDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<PersonalAttendanceDetailDTO> personalAttendance = new List<PersonalAttendanceDetailDTO>();

                var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == isDeleted)
                            join ats in db.ATTENDANCE_STATUS on p.attendanceStatusID equals ats.attendanceStatusID
                            join m in db.MEMBER.Where(x => x.isDeleted == false) on p.memberID equals m.memberID
                            join mo in db.MONTH on p.monthID equals mo.monthID
                            join g in db.GENDER on m.genderID equals g.genderID
                            select new
                            {
                                attendanceID = p.attendanceID,
                                attendanceStatusID = p.attendanceStatusID,
                                attendanceStatusName = ats.attendanceStatus,
                                day = p.day,
                                monthID = p.monthID,
                                monthName = mo.monthName,
                                year = p.year,
                                memberID = p.memberID,
                                surname = m.surname,
                                name = m.name,
                                imagePath = m.imagePath,
                                genderID = m.genderID,
                                genderName = g.genderName,
                                monthlyDue = p.monthlyDues,
                                expectedMonthlyDue = p.expectedMonthlyDue,
                                balance = p.balance,
                                generalAttendanceID = p.generalAttendanceID,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                    dto.AttendanceID = item.attendanceID;
                    dto.AttendanceStatusID = item.attendanceStatusID;
                    dto.AttendanceStatusName = item.attendanceStatusName;
                    dto.Day = (int)item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.MemberID = item.memberID;
                    dto.Surname = item.surname;
                    dto.Name = item.name;
                    dto.ImagePath = item.imagePath;
                    dto.GenderID = item.genderID;
                    dto.Gender = item.genderName;
                    dto.MonthlyDue = (decimal)item.monthlyDue;
                    dto.ExpectedDue = (decimal)item.expectedMonthlyDue;
                    dto.Balance = (decimal)item.balance;
                    dto.GeneralAttendanceID = item.generalAttendanceID;
                    personalAttendance.Add(dto);
                }
                return personalAttendance;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<PersonalAttendanceDetailDTO> SelectMembers(int generalAttendanceID)
        {
            try
            {
                List<PersonalAttendanceDetailDTO> personalAttendance = new List<PersonalAttendanceDetailDTO>();

                var list = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.generalAttendanceID == generalAttendanceID)
                            join ats in db.ATTENDANCE_STATUS on p.attendanceStatusID equals ats.attendanceStatusID
                            join m in db.MEMBER on p.memberID equals m.memberID
                            join mo in db.MONTH on p.monthID equals mo.monthID
                            join g in db.GENDER on m.genderID equals g.genderID
                            select new
                            {
                                attendanceID = p.attendanceID,
                                attendanceStatusID = p.attendanceStatusID,
                                attendanceStatusName = ats.attendanceStatus,
                                day = p.day,
                                monthID = p.monthID,
                                monthName = mo.monthName,
                                year = p.year,
                                memberID = p.memberID,
                                surname = m.surname,
                                name = m.name,
                                imagePath = m.imagePath,
                                genderID = m.genderID,
                                genderName = g.genderName,
                                monthlyDue = p.monthlyDues,
                                expectedMonthlyDue = p.expectedMonthlyDue,
                                balance = p.balance,
                                generalAttendanceID = p.generalAttendanceID,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                    dto.AttendanceID = item.attendanceID;
                    dto.AttendanceStatusID = item.attendanceStatusID;
                    dto.AttendanceStatusName = item.attendanceStatusName;
                    dto.Day = (int)item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.MemberID = item.memberID;
                    dto.Surname = item.surname;
                    dto.Name = item.name;
                    dto.ImagePath = item.imagePath;
                    dto.GenderID = item.genderID;
                    dto.Gender = item.genderName;
                    dto.MonthlyDue = (decimal)item.monthlyDue;
                    dto.ExpectedDue = (decimal)item.expectedMonthlyDue;
                    dto.Balance = (decimal)item.balance;
                    dto.GeneralAttendanceID = item.generalAttendanceID;
                    personalAttendance.Add(dto);
                }
                return personalAttendance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelectLastMeetingAttendance()
        {
            try
            {
                List<int> lastAttendance = new List<int>();
                var lastMeeting = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x=>x.year).OrderByDescending(x => x.monthID).FirstOrDefault();
                if (lastMeeting != null)
                {
                    var list = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.year == lastMeeting.year && x.monthID == lastMeeting.monthID && x.day == lastMeeting.day);
                    foreach (var item in list)
                    {
                        PersonalAttendanceDetailDTO dto = new PersonalAttendanceDetailDTO();
                        if (item.attendanceStatusID == 2)
                        {
                            lastAttendance.Add(item.memberID);
                        }                        
                    }
                    int result = lastAttendance.Count();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PERSONAL_ATTENDANCE> IsUnique(int personID, int attendanceID)
        {
            return db.PERSONAL_ATTENDANCE.Where(x => x.memberID == personID && x.generalAttendanceID == attendanceID).ToList();
        }

        public bool Update(PERSONAL_ATTENDANCE entity)
        {
            try
            {
                PERSONAL_ATTENDANCE personalAttendance = db.PERSONAL_ATTENDANCE.First(x=>x.attendanceID == entity.attendanceID);
                personalAttendance.attendanceStatusID = entity.attendanceStatusID;
                personalAttendance.memberID = entity.memberID;
                personalAttendance.monthlyDues = entity.monthlyDues;
                personalAttendance.expectedMonthlyDue = entity.expectedMonthlyDue;
                personalAttendance.balance = entity.balance;
                personalAttendance.day = entity.day;
                personalAttendance.monthID = entity.monthID;
                personalAttendance.generalAttendanceID = entity.generalAttendanceID;
                personalAttendance.year = entity.year;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
