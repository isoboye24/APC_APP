using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class MonthService : IMonthService
    {
        private readonly IMonthRepository _repository;
        public MonthService(IMonthRepository repository)
        {
            _repository = repository;
        }

        public List<MonthDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new MonthDTO
                {
                    MonthId = x.monthID,
                    MonthName = x.monthName
                })
                .OrderBy(x => x.MonthName)
                .ToList();
        }
    }
}
