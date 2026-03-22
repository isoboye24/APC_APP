using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IDocumentRepository
    {
        IQueryable<DOCUMENT> GetAll();
        IQueryable<DOCUMENT> GetAllDeletedDocuments();
        IQueryable<DOCUMENT> GetById(int id);
        bool Insert(Document data);
        bool Update(Document data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string documentPath);
        int Count();
    }
}
