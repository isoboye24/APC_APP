using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;
namespace APC.Applications.Interfaces
{
    public interface IPaymentStatusRepository
    {
        IQueryable<PAYMENT_STATUS> GetAll();
        IQueryable<PAYMENT_STATUS> GetAllDeletedPaymentStatuses();
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
