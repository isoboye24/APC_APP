using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        List<Document> GetAll();
        Document GetById(int id);
        bool Insert(Document data);
        bool Update(Document data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string documentPath);
        int Count();
    }
}
