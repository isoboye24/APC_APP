using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IMembershipStatusRepository
    {
        IQueryable<MEMBERSHIP_STATUS> GetAll();
        IQueryable<MEMBERSHIP_STATUS> GetByStatus(string status);
        MembershipStatus GetById(int id);        
    }
}
