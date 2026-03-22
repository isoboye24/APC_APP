using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class EventReceiptService : IEventReceiptService
    {
        private readonly IEventReceiptRepository _repository;
        public EventReceiptService(IEventReceiptRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(EventReceipt data)
        {
            if (_repository.Exists(data.EventId, data.ImagePath))
                throw new Exception("Event receipt already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EventReceipt> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EventReceipt data)
        {
            var check = _repository.GetById(data.EventReceiptId);
            if (check == null)
                throw new Exception("Event receipt not found");

            data.UpdateImageCaption(data.Caption);
            data.UpdateSummary(data.Summary);
            data.UpdateImagePath(data.ImagePath);
            data.UpdateDate(data.ReceiptDate);
            data.UpdateSpentAmount(data.AmountSpent);

            return _repository.Update(data);
        }
    }
}
