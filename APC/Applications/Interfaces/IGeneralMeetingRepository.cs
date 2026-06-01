using APC.Infrastructure.Data;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IGeneralMeetingRepository
    {
        IQueryable<GENERAL_ATTENDANCE> GetAll();
        IQueryable<GENERAL_ATTENDANCE> GetAllDeletedGeneralMeetings();
        IQueryable<GENERAL_ATTENDANCE> GetById(int id);
        bool Insert(GeneralMeeting data);
        bool Update(GeneralMeeting data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int month, int year);
        int Count();
    }
}
