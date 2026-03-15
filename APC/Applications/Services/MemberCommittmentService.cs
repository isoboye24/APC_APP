using APC.Applications.DTO;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class MemberCommittmentService : IMemberCommittmentService
    {
        private readonly IMemberCommittmentRepository _repository;
        public MemberCommittmentService(IMemberCommittmentRepository repository)
        {
            _repository = repository;
        }

        public List<MemberCommittment> GetMembersCommittment(int year)
            => _repository.GetMembersCommittment(year);
    }
}
