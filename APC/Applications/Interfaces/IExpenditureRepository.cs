using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IExpenditureRepository
    {
        List<Expenditure> GetAll();
        Expenditure GetById(int id);
        bool Insert(Expenditure data);
        bool Update(Expenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(decimal spentAmount, string summary, DateTime date);
        int Count();
    }
}
