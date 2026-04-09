using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class ExpenditureRepository : IExpenditureRepository
    {
        private readonly APCEntities _db;
        public ExpenditureRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EXPENDITURE.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EXPENDITURE.First(x => x.expenditureID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(decimal spentAmount, string summary, DateTime date)
        {
            return _db.EXPENDITURE.Any(x => !x.isDeleted && x.amountSpent == spentAmount && x.summary == summary 
                                            && x.expenditureDate.Day == date.Day && x.expenditureDate.Month == date.Month
                                            && x.expenditureDate.Year == date.Year);
        }

        public IQueryable<EXPENDITURE> GetAll()
        {
            return _db.EXPENDITURE.Where(x => !x.isDeleted);
        }
        
        public IQueryable<EXPENDITURE> GetAllDeletedExpenditures()
        {
            return _db.EXPENDITURE.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EXPENDITURE.First(x => x.expenditureID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<EXPENDITURE> GetById(int id)
        {
            return _db.EXPENDITURE.Where(x => !x.isDeleted && x.expenditureID == id);
        }

        public bool Insert(Expenditure data)
        {
            _db.EXPENDITURE.Add(new EXPENDITURE
            {
                amountSpent = data.AmountSpent,
                summary = data.Summary,
                expenditureDate = data.ExpenditureDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EXPENDITURE.FirstOrDefault(x => x.expenditureID == id);

            if (entity == null)
                return false;

            _db.EXPENDITURE.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Expenditure data)
        {
            var entity = _db.EXPENDITURE.First(x => x.expenditureID == data.ExpenditureId);
            entity.amountSpent = data.AmountSpent;
            entity.summary = data.Summary;
            entity.expenditureDate = data.ExpenditureDate;

            _db.SaveChanges();
            return true;
        }
    }
}
