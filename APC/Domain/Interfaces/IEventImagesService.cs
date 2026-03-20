using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IEventImagesService
    {
        List<EventImageDTO> GetAll();
        List<EventImageDTO> GetAllDeletedEventImages();
        bool Create(EventImages data);
        bool Update(EventImages data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
