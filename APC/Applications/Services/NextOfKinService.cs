using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

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
