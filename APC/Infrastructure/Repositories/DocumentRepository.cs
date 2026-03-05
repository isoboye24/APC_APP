using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

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

        public List<Document> GetAll()
        {
            var data = _db.DOCUMENT
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .OrderByDescending(x => x.monthID)
                .OrderByDescending(x => x.day)
                .ThenBy(x => x.documentName)
                .ToList();

            return data
                .Select(x => Document.Rehydrate(
                    x.documentID,
                    x.documentName,
                    x.documentPath,
                    x.documentType,
                    x.day,
                    x.monthID,
                    x.year
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.DOCUMENT.First(x => x.documentID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Document GetById(int id)
        {
            var entity = _db.DOCUMENT
                .Where(x => x.documentID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.documentID,
                    x.documentName,
                    x.documentPath,
                    x.documentType,
                    x.day,
                    x.monthID,
                    x.year
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return Document.Rehydrate(
                    entity.documentID,
                    entity.documentName,
                    entity.documentPath,
                    entity.documentType,
                    entity.day,
                    entity.monthID,
                    entity.year
            );
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
