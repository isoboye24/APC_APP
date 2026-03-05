using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Document> GetAll()
            => _repository.GetAll();

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
