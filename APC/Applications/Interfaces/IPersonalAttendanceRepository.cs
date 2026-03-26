using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IPersonalAttendanceRepository
    {
        IQueryable<PERSONAL_ATTENDANCE> GetAllByGeneralMeetingId(int id);
        IQueryable<PERSONAL_ATTENDANCE> GetAllDeletedPersonalAttendance();
        IQueryable<PERSONAL_ATTENDANCE> GetById(int id);
        bool Insert(PersonalAttendance data);
        bool Update(PersonalAttendance data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int memberId, int generalMeetingId);
        int Count();
    }
}
