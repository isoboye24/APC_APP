using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
