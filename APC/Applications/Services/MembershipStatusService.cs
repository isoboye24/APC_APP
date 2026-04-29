using APC.Applications.DTO;
using APC.Applications.Entities;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.Applications.Services
{
    internal class MembershipStatusService : IMembershipStatusService
    {
        private readonly IMembershipStatusRepository _repository;
        public MembershipStatusService(IMembershipStatusRepository repository)
        {
            _repository = repository;
        }

        public List<MembershipStatusDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new MembershipStatusDTO
                {
                    MembershipStatusId = x.membershipStatusID,
                    MembershipStatusName = x.membershipStatus
                })
                .OrderBy(x => x.MembershipStatusName)
                .ToList();
        }

        public MembershipStatus GetById(int id)
            => _repository.GetById(id);
    }
}
