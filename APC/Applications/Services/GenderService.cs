using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _repository;
        public GenderService(IGenderRepository repository)
        {
            _repository = repository;
        }

        public List<Gender> GetAll()
            => _repository.GetAll();
    }
}
