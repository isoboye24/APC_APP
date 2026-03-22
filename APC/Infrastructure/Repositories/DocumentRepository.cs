using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly APCEntities _db;
        public DocumentRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.DOCUMENT.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.DOCUMENT.First(x => x.documentID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string documentPath)
        {
            return _db.DOCUMENT.Any(x => !x.isDeleted && x.documentPath == documentPath);
        }

        public IQueryable<DOCUMENT> GetAll()
        {
            return _db.DOCUMENT.Where(x => !x.isDeleted);
        }

        public IQueryable<DOCUMENT> GetAllDeletedDocuments()
        {
            return _db.DOCUMENT.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.DOCUMENT.First(x => x.documentID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<DOCUMENT> GetById(int id)
        {
            return _db.DOCUMENT.Where(x => !x.isDeleted && x.documentID == id);
        }

        public bool Insert(Document data)
        {
            _db.DOCUMENT.Add(new DOCUMENT
            {
                documentName = data.DocumentName,
                documentPath = data.DocumentPath,
                documentType = data.DocumentType,
                day = data.Day,
                monthID = data.MonthId,
                year = data.Year,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.DOCUMENT.FirstOrDefault(x => x.documentID == id);

            if (entity == null)
                return false;

            _db.DOCUMENT.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Document data)
        {
            var entity = _db.DOCUMENT.First(x => x.documentID == data.DocumentId);
            entity.documentName = data.DocumentName;
            entity.documentPath = data.DocumentPath;
            entity.documentType = data.DocumentType;
            entity.day = data.Day;
            entity.monthID = data.MonthId;
            entity.year = data.Year;

            _db.SaveChanges();
            return true;
        }
    }
}
