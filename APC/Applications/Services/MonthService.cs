using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class MonthService : IMonthService
    {
        private readonly IMonthService _repository;
        public MonthService(IMonthService repository)
        {
            _repository = repository;
        }

        public List<Month> GetAll()
            => _repository.GetAll();
    }
}
