using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IPaymentStatusService
    {
        List<PaymentStatusDTO> GetAll();
        bool Create(PaymentStatus status);
        bool Update(PaymentStatus status);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
