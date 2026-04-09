using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System.Collections.Generic;
using APC.Applications.Entities;

namespace APC.Applications.Services
{
    internal class MembershipStatusService : IMembershipStatusService
    {
        private readonly IMembershipStatusRepository _repository;
        public MembershipStatusService(IMembershipStatusRepository repository)
        {
            _repository = repository;
        }
        public List<MembershipStatus> GetAll()
            => _repository.GetAll();

        public MembershipStatus GetById(int id)
            => _repository.GetById(id);
    }
}
