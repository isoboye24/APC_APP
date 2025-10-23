using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    internal class PersonalAttendanceBLL : IBLL<PersonalAttendanceDTO, PersonalAttendanceDetailDTO>
    {
        MemberDAO memberDAO = new MemberDAO();
        GenderDAO genderDAO = new GenderDAO();
        AttendanceStatusDAO attendStatusDAO = new AttendanceStatusDAO();
        MonthDAO monthDAO = new MonthDAO();
        PersonalAttendanceDAO dao = new PersonalAttendanceDAO();
        FinedMemberDAO finedMemeberDAO = new FinedMemberDAO();
        public bool Delete(PersonalAttendanceDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(PersonalAttendanceDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PersonalAttendanceDetailDTO entity)
        {
            PERSONAL_ATTENDANCE attendance = new PERSONAL_ATTENDANCE();
            attendance.attendanceStatusID = entity.AttendanceStatusID;
            attendance.memberID = entity.MemberID;
            attendance.monthlyDues = entity.MonthlyDue;
            attendance.expectedMonthlyDue = entity.ExpectedDue;
            attendance.balance = entity.Balance;
            attendance.day = entity.Day;
            attendance.monthID = entity.MonthID;
            attendance.generalAttendanceID = entity.GeneralAttendanceID;
            attendance.year = Convert.ToInt32(entity.Year);
            return dao.Insert(attendance);
        }
        public PersonalAttendanceDTO Select()
        {
            PersonalAttendanceDTO dto = new PersonalAttendanceDTO();
            dto.Members = memberDAO.Select();
            dto.AttendanceStatuses = attendStatusDAO.Select();
            return dto;
        }
        
        public PersonalAttendanceDTO Select(int ID)
        {
            PersonalAttendanceDTO dto = new PersonalAttendanceDTO();
            dto.Months = monthDAO.Select();
            dto.AttendanceStatuses = attendStatusDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Years = dao.SelectOnlyYears();
            dto.Members = memberDAO.Select();
            dto.PresentMember = dao.SelectPresentMember(ID);
            dto.AbsentMember = dao.SelectAbsentMember(ID);
            dto.AmountsContributed = dao.AmountsContributed(ID);
            dto.AmountExpected = dao.AmountsExpected(ID);
            dto.AmountsBalance = dao.AmountsBalance(ID);
            dto.FinedMember = finedMemeberDAO.SelectFinedMember(ID);
            return dto;
        }

        public PersonalAttendanceDTO SelectMembersSet(int ID)
        {
            PersonalAttendanceDTO dto = new PersonalAttendanceDTO();            
            dto.PersonalAttendances = dao.SelectMembers(ID);
            return dto;
        }

        public int SelectLastMeetingAttendance()
        {
            return dao.SelectLastMeetingAttendance();
        }

        public decimal TotalDuesExpected(int meetingID)
        {
            return dao.AmountExpected(meetingID);
        }

        public decimal DuesContributed(int meetingID)
        {
            return dao.DuesContributed(meetingID);
        }

        public bool IsUnique(int personID, int attendanceID)
        {
            List<PERSONAL_ATTENDANCE> list = dao.IsUnique(personID, attendanceID);
            if (list.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Update(PersonalAttendanceDetailDTO entity)
        {
            PERSONAL_ATTENDANCE attendance = new PERSONAL_ATTENDANCE();
            attendance.attendanceID = entity.AttendanceID;
            attendance.attendanceStatusID = entity.AttendanceStatusID;
            attendance.memberID = entity.MemberID;
            attendance.monthlyDues = entity.MonthlyDue;
            attendance.expectedMonthlyDue = entity.ExpectedDue;
            attendance.balance = entity.Balance;
            attendance.day = entity.Day;
            attendance.monthID = entity.MonthID;
            attendance.generalAttendanceID = entity.GeneralAttendanceID;
            attendance.year = Convert.ToInt32(entity.Year);
            return dao.Update(attendance);
        }
    }
}
