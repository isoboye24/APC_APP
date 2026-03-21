using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IPersonalAttendanceRepository
    {
        List<PersonalAttendance> GetAll();
        PersonalAttendance GetById(int id);
        bool Insert(PersonalAttendance data);
        bool Update(PersonalAttendance data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int memberId, int generalMeetingId);
        int Count();
        List<PersonalAttendance> GetAllDeleted();
        List<PersonalAttendanceFullDetails> GetFullPersonalAttendanceDetails();
    }
}
