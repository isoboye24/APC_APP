using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IEventImagesRepository
    {
        IQueryable<EVENT_IMAGE> GetAll(int id);
        IQueryable<EVENT_IMAGE> GetAllDeletedEventImages();
        IQueryable<EVENT_IMAGE> GetById(int id);
        bool Insert(EventImages data);
        bool Update(EventImages data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, string imagePath);
        int Count();
    }
}
