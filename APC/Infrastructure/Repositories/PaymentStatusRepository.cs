using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        private readonly APCEntities _db;
        public PaymentStatusRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.PAYMENT_STATUS.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.PAYMENT_STATUS.First(x => x.paymentStatusID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.PAYMENT_STATUS.Any(x => !x.isDeleted && x.paymentStatusName == name);
        }

        public List<PaymentStatus> GetAll()
        {
            var data = _db.PAYMENT_STATUS
                .Where(x => !x.isDeleted)
                .OrderBy(x => x.paymentStatusName)
                .ToList();

            return data
                .Select(x => PaymentStatus.Rehydrate(
                    x.paymentStatusID,
                    x.paymentStatusName
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.PAYMENT_STATUS.First(x => x.paymentStatusID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public PaymentStatus GetById(int id)
        {
            var entity = _db.PAYMENT_STATUS.FirstOrDefault(x => x.paymentStatusID == id && !x.isDeleted);
            if (entity == null) return null;

            var paymentStatus = new PaymentStatus(entity.paymentStatusName);
            paymentStatus.SetId(entity.paymentStatusID);
            return paymentStatus;
        }

        public bool Insert(PaymentStatus status)
        {
            _db.PAYMENT_STATUS.Add(new PAYMENT_STATUS
            {
                paymentStatusName = status.PaymentStatusName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.PAYMENT_STATUS.FirstOrDefault(x => x.paymentStatusID == id);

            if (entity == null)
                return false;

            _db.PAYMENT_STATUS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(PaymentStatus status)
        {
            var entity = _db.PAYMENT_STATUS.First(x => x.paymentStatusID == status.PaymentStatusId);
            entity.paymentStatusName = status.PaymentStatusName;
            _db.SaveChanges();
            return true;
        }
    }
}
