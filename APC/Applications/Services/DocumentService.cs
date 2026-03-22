using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
        }
        public int Count()
            => _repository.Count();

        public bool Create(Document data)
        {
            if (_repository.Exists(data.DocumentPath))
                throw new Exception("Document already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<DocumentDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new DocumentDTO
                {
                    DocumentId = x.documentID,
                    DocumentName = x.documentName,
                    DocumentPath = x.documentPath,
                    DocumentType = x.documentType,
                    Date = new DateTime(x.year, x.monthID, x.day),
                    FormattedDate = new DateTime(x.year, x.monthID, x.day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date.Year)
                .ThenByDescending(x => x.Date.Month)
                .ThenByDescending(x => x.Date.Day)
                .ThenBy(x => x.DocumentName)
                .ToList();
        }
        
        public List<DocumentDTO> GetAllDeletedDocuments()
        {
            return _repository.GetAllDeletedDocuments()
                .Select(x => new DocumentDTO
                {
                    DocumentId = x.documentID,
                    DocumentName = x.documentName,
                    DocumentPath = x.documentPath,
                    DocumentType = x.documentType,
                    Date = new DateTime(x.year, x.monthID, x.day),
                    FormattedDate = new DateTime(x.year, x.monthID, x.day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date.Year)
                .ThenByDescending(x => x.Date.Month)
                .ThenByDescending(x => x.Date.Day)
                .ThenBy(x => x.DocumentName)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Document data)
        {
            var check = _repository.GetById(data.DocumentId);
            if (check == null)
                throw new Exception("Document not found");

            data.UpdateDocumentName(data.DocumentName);
            data.UpdateDocumentPath(data.DocumentPath);

            return _repository.Update(data);
        }
    }
}
