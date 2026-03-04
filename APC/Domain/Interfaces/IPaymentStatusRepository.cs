using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IPaymentStatusRepository
    {
        List<PaymentStatus> GetAll();
        PaymentStatus GetById(int id);
        bool Insert(PaymentStatus status);
        bool Update(PaymentStatus status);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
