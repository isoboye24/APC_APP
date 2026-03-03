using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IGenderRepository
    {
        List<Gender> GetAll();
        Gender GetById(int id);
    }
}
