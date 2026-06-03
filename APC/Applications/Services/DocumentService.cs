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
                .ToList()
                .Select(x => new DocumentDTO
                {
                    DocumentId = x.documentID,
                    DocumentName = x.documentName,
                    DocumentPath = x.documentPath,
                    DocumentType = x.documentType,
                    Date = new DateTime(x.year, x.monthID, x.day),
                    FormattedDate = new DateTime(x.year, x.monthID, x.day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public List<DocumentDTO> GetDocumentByYear(int year)
        {
            return _repository.GetAll()
                .Where(x => x.year == year)
                .ToList()
                .Select(x => new DocumentDTO
                {
                    DocumentId = x.documentID,
                    DocumentName = x.documentName,
                    DocumentPath = x.documentPath,
                    DocumentType = x.documentType,
                    Date = new DateTime(x.year, x.monthID, x.day),
                    FormattedDate = new DateTime(x.year, x.monthID, x.day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date)                
                .ToList();
        }
        
        public List<DocumentDTO> GetAllDeletedDocuments()
        {
            return _repository.GetAllDeletedDocuments()
                .ToList()
                .Select(x => new DocumentDTO
                {
                    DocumentId = x.documentID,
                    DocumentName = x.documentName,
                    DocumentPath = x.documentPath,
                    DocumentType = x.documentType,
                    Date = new DateTime(x.year, x.monthID, x.day),
                    FormattedDate = new DateTime(x.year, x.monthID, x.day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date)
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

            return _repository.Update(data);
        }

        public List<YearDTO> GetOnlyYears()
        {
            return _repository.GetAll()
                .Select(x => x.year)
                .Distinct()
                .OrderByDescending(x => x)
                .Select(x => new YearDTO
                {
                    YearInValue = x,
                    YearInText = x.ToString()
                })
                .ToList();
        }
    }
}
