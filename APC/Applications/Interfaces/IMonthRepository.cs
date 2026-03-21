using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IMonthRepository
    {
        IQueryable<MONTH> GetAll();
        Month GetById(int id);
    }
}
