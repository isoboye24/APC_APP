using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class RelationshipToNextOfKinService : IRelationshipToNextOfKinService
    {
        private readonly INextOfKinRepository _repository;
        public RelationshipToNextOfKinService(INextOfKinRepository repository)
        {
            _repository = repository;
        }

        public List<RelationshipToNextOfKinDTO> GetAll()
        {
            return _repository.GetAll()
                 .ToList()
                .Select(x => new RelationshipToNextOfKinDTO
                {
                    RelationshipToNextOfKinId = x.RelationshipToKinID,
                    RelationshipToNextOfKin = x.RelationshipToKin
                }).OrderBy(x => x.RelationshipToNextOfKin)
                .ToList();
        }
    }
}
