using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IDocumentService
    {
        List<DocumentDTO> GetAll();
        List<DocumentDTO> GetAllDeletedDocuments();
        bool Create(Document data);
        bool Update(Document data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
