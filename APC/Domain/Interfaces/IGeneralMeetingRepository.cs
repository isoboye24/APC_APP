using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IGeneralMeetingRepository
    {
        List<GeneralMeeting> GetAll();
        GeneralMeeting GetById(int id);
        bool Insert(GeneralMeeting data);
        bool Update(GeneralMeeting data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int month, int year);
        int Count();
        List<GeneralMeeting> GetAllDeleted();
    }
}
