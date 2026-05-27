using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IEventsService
    {
        List<EventDTO> GetAll();
        List<EventDTO> GetAnnualEvents(int year);
        bool Create(TheEvents data);
        bool Update(TheEvents data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
