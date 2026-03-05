using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IConstitutionRepository
    {
        List<Constitution> GetAll();
        Constitution GetById(int id);
        bool Insert(Constitution data);
        bool Update(Constitution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string constitutionText, decimal fine);
        int Count();
    }
}
