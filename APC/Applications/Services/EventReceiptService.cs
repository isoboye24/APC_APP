using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using APC.Applications.DTO;
using System.Linq;

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

        public List<EventReceiptDTO> GetAll()
        {
            return _repository.GetAll()
               .OrderBy(x => x.eventID)
               .ToList()
                .Select((x, index) => new EventReceiptDTO
                {
                    Counter = index + 1,
                    EventReceiptId = x.eventReceiptID,
                    EventId = x.eventID,
                    Summary = x.summary,
                    ImagePath = x.imagePath,
                    Caption = x.caption,
                    FormattedReceiptDate = x.receiptDate.ToString("dd.MM.yyyy"),
                    ReceiptDate = x.receiptDate,
                    AmountSpent = x.amountSpent,
                    FormattedAmountSpent = x.amountSpent.ToString() + " €",
                })
                .ToList();
        }
        
        public List<EventReceiptDTO> GetByEvent(int eventId)
        {
            return _repository.GetAll()
                .Where(x => x.eventID == eventId)
               .OrderBy(x => x.caption)
               .ToList()
                .Select((x, index) => new EventReceiptDTO
                {
                    Counter = index + 1,
                    EventReceiptId = x.eventReceiptID,
                    EventId = x.eventID,
                    Summary = x.summary,
                    ImagePath = x.imagePath,
                    Caption = x.caption,
                    FormattedReceiptDate = x.receiptDate.ToString("dd.MM.yyyy"),
                    ReceiptDate = x.receiptDate,
                    AmountSpent = x.amountSpent,
                    FormattedAmountSpent = x.amountSpent.ToString() + " €",
                })
                .ToList();
        }
        
        public List<EventReceiptDTO> GetAllDeletedEventReceipts()
        {
            return _repository.GetAllDeletedEventReceipts()
               .OrderBy(x => x.caption)
               .ToList()
                .Select((x, index) => new EventReceiptDTO
                {
                    Counter = index + 1,
                    EventReceiptId = x.eventReceiptID,
                    EventId = x.eventID,
                    Summary = x.summary,
                    ImagePath = x.imagePath,
                    Caption = x.caption,
                    AmountSpent = x.amountSpent,
                    FormattedAmountSpent = x.amountSpent.ToString() + " €",
                })
                .ToList();
        }

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

        public int GetEventReceiptsCountByEvent(int eventId)
        {
            return _repository.GetAll()
                                .Where(x => x.eventID == eventId)
                                .Count();
        }

        public int GetAllEventReceiptsCount()
        {
            return _repository.GetAll()
                                .Count();
        }
    }
}
