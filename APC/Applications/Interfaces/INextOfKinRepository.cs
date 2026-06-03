using APC.Infrastructure.Data;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface INextOfKinRepository
    {
        IQueryable<NEXT_OF_KIN_RELATIONSHIP> GetAll();
    }
}
