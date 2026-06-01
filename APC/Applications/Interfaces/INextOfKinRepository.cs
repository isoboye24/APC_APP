using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface INextOfKinRepository
    {
        List<NextOfKin> GetAll();
        NextOfKin GetById(int id);
    }
}
