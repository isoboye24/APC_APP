using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IGeneralMeetingService
    {
        List<GeneralMeeting> GetAll();
        bool Create(GeneralMeeting data);
        bool Update(GeneralMeeting data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        List<GeneralMeeting> GetAllDeleted();
    }
}
