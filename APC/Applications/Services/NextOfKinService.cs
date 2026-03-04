using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class NextOfKinService : INextOfKinService
    {
        private readonly INextOfKinRepository _repository;
        public NextOfKinService(INextOfKinRepository repository)
        {
            _repository = repository;
        }

        public List<NextOfKin> GetAll()
            => _repository.GetAll();
    }
}
