using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System.Collections.Generic;


namespace APC.Applications.Services
{
    public class MemberCommittmentService : IMemberCommittmentService
    {
        private readonly IMemberCommittmentRepository _repository;
        public MemberCommittmentService(IMemberCommittmentRepository repository)
        {
            _repository = repository;
        }

        public List<MemberCommittmentDTO> GetMembersCommittment(int year)
            => _repository.GetMembersCommittment(year);
    }
}
