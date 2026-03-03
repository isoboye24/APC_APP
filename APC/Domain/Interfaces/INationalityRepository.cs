using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface INationalityRepository
    {
        List<Nationality> GetAll();
        Nationality GetById(int id);
        bool Insert(Nationality nationality);
        bool Update(Nationality nationality);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
