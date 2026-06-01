using APC.Infrastructure.Data;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IMembershipStatusRepository
    {
        IQueryable<MEMBERSHIP_STATUS> GetAll();
        IQueryable<MEMBERSHIP_STATUS> GetByStatus(string status);
        MembershipStatus GetById(int id);        
    }
}
