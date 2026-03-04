using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IPaymentStatusService
    {
        List<PaymentStatus> GetAll();
        bool Create(PaymentStatus status);
        bool Update(PaymentStatus status);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
