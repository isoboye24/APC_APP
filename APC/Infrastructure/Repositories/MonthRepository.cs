using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class MonthRepository : IMonthRepository
    {
        private readonly APCEntities _db;
        public MonthRepository(APCEntities db)
        {
            _db = db;
        }
        public List<Month> GetAll()
        {
            var data = _db.MONTH
                .OrderBy(x => x.monthID)
                .ToList();

            return data
                .Select(x => Month.Rehydrate(
                    x.monthID,
                    x.monthName
                ))
                .ToList();
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
