using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly IPaymentStatusRepository _repository;
        public PaymentStatusService(IPaymentStatusRepository repository)
        {
            _repository = repository;
        }
        public int Count()
            => _repository.Count();

        public bool Create(PaymentStatus status)
        {
            if (_repository.Exists(status.PaymentStatusName))
                throw new Exception("Payment status already exists");

            return _repository.Insert(status);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<PaymentStatusDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new PaymentStatusDTO
                {
                    PaymentStatusId = x.paymentStatusID,
                    PaymentStatusName = x.paymentStatusName
                }).OrderBy(x => x.PaymentStatusName)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.GetBack(id);

        public bool Update(PaymentStatus status)
        {
            var check = _repository.GetById(status.PaymentStatusId);
            if (check == null)
                throw new Exception("Payment status not found");

            status.UpdateName(status.PaymentStatusName);
            return _repository.Update(status);
        }
    }
}
