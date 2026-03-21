using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IGenderRepository
    {
        IQueryable<GENDER> GetAll();
        Gender GetById(int id);
    }
}
