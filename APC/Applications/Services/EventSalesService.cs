using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class EventSalesService : IEventSalesService
    {
        private readonly IEventSalesRepository _repository;

        public EventSalesService(IEventSalesRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(EventSales data)
        {
            if (_repository.Exists(data.EventId, data.AmountSold, data.Summary, data.Day, data.MonthId, data.Year))
                throw new Exception("Event sales already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EventSales> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EventSales data)
        {
            var check = _repository.GetById(data.EventSalesId);
            if (check == null)
                throw new Exception("Event sales not found");

            data.UpdateSummary(data.Summary);
            data.UpdateDate(data.SalesDate);
            data.UpdateAmountSold(data.AmountSold);

            return _repository.Update(data);
        }
    }
}
