using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IGeneralMeetingService
    {
        List<GeneralMeetingDTO> GetAll();
        bool Create(GeneralMeeting data);
        bool Update(GeneralMeeting data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        List<GeneralMeetingDTO> GetAllDeleted();
    }
}
