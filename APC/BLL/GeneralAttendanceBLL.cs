using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.CompilerServices;

namespace APC.BLL
{
    public class GeneralAttendanceBLL : IBLL<GeneralAttendanceDTO, GeneralAttendanceDetailDTO>
    {
        GeneralAttendanceDAO dao = new GeneralAttendanceDAO();
        PersonalAttendanceDAO personalAttendanceDAO = new PersonalAttendanceDAO();
        MonthDAO monthDAO = new MonthDAO();
        FinedMemberDAO fineDAO = new FinedMemberDAO();
        public bool Delete(GeneralAttendanceDetailDTO entity)
        {
            GENERAL_ATTENDANCE generalAttendance = new GENERAL_ATTENDANCE();
            generalAttendance.generalAttendanceID = entity.GeneralAttendanceID;
            dao.Delete(generalAttendance);
            PERSONAL_ATTENDANCE personalAttendance = new PERSONAL_ATTENDANCE();
            personalAttendance.generalAttendanceID = entity.GeneralAttendanceID;
            personalAttendanceDAO.Delete(personalAttendance);
            return true;
        }

        public bool GetBack(GeneralAttendanceDetailDTO entity)
        {
            return dao.GetBack(entity.GeneralAttendanceID);
        }

        public bool Insert(GeneralAttendanceDetailDTO entity)
        {
            GENERAL_ATTENDANCE generalAttendance = new GENERAL_ATTENDANCE();
            generalAttendance.day = entity.Day;
            generalAttendance.monthID = entity.MonthID;
            generalAttendance.year = Convert.ToInt32(entity.Year);
            generalAttendance.totalMembersPresent = entity.TotalMembersPresent;
            generalAttendance.totalMembersAbsent = entity.TotalMembersAbsent;
            generalAttendance.totalDuesPaid = entity.TotalDuesPaid;
            generalAttendance.totalDuesExpected = entity.TotalDuesExpected;
            generalAttendance.totalDuesBalance = entity.TotalDuesBalance;
            generalAttendance.summary = entity.Summary;
            generalAttendance.attendanceDate = entity.AttendanceDate;
            return dao.Insert(generalAttendance);
        }

        public GeneralAttendanceDTO Select()
        {
            throw new NotImplementedException();
        }
        public GeneralAttendanceDTO Select(int year)
        {
            GeneralAttendanceDTO dto = new GeneralAttendanceDTO();
            dto.Months = monthDAO.Select();
            dto.GeneralAttendance = dao.Select(year);
            dto.Years = dao.SelectOnlyMeetingYears();
            return dto;
        }

        public decimal SelectMonthlyDues(int month)
        {
            return dao.SelectMonthlyDues(month);
        }
        public bool CheckMeeting(int day, int month, int year)
        {
            return dao.CheckMeeting(day, month, year);
        }

        public decimal SelectYearlyDues(int year)
        {
            return dao.SelectYearlyDues(year);
        }
        public decimal TotalPaidFines()
        {
            return fineDAO.TotalPaidFines();
        }

        public bool Update(GeneralAttendanceDetailDTO entity)
        {
            GENERAL_ATTENDANCE generalAttendance = new GENERAL_ATTENDANCE();
            generalAttendance.generalAttendanceID = entity.GeneralAttendanceID;
            generalAttendance.day = entity.Day;
            generalAttendance.monthID = entity.MonthID;
            generalAttendance.year = Convert.ToInt32(entity.Year);
            generalAttendance.totalMembersPresent = entity.TotalMembersPresent;
            generalAttendance.totalMembersAbsent = entity.TotalMembersAbsent;
            generalAttendance.totalDuesPaid = entity.TotalDuesPaid;
            generalAttendance.totalDuesExpected = entity.TotalDuesExpected;
            generalAttendance.totalDuesBalance = entity.TotalDuesBalance;
            generalAttendance.summary = entity.Summary;
            generalAttendance.attendanceDate = entity.AttendanceDate;
            return dao.Update(generalAttendance);
        }
    }
}