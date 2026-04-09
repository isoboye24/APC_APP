using APC.DAL;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IGenderRepository
    {
        IQueryable<GENDER> GetAll();
        IQueryable<GENDER> GetById(int id);
    }
}
