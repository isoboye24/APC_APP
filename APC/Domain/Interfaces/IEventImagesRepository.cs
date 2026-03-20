using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Domain.Interfaces
{
    public interface IEventImagesRepository
    {
        IQueryable<EVENT_IMAGE> GetAll();
        IQueryable<EVENT_IMAGE> GetAllDeletedEventImages();
        EventImages GetById(int id);
        bool Insert(EventImages data);
        bool Update(EventImages data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, string imagePath);
        int Count();
    }
}
