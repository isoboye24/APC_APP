using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class MonthRepository : IMonthRepository
    {
        private readonly APCEntities _db;
        public MonthRepository(APCEntities db)
        {
            _db = db;
        }
        public IQueryable<MONTH> GetAll()
        {
            return _db.MONTH;
        }

        public Month GetById(int id)
        {
            var entity = _db.MONTH.FirstOrDefault(x => x.monthID == id);
            if (entity == null) return null;

            var month = new Month(entity.monthName);
            month.SetId(entity.monthID);
            return month;
        }
    }
}
