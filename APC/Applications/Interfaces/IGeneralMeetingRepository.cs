using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
