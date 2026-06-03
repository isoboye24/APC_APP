using APC.Applications.DTO;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IRelationshipToNextOfKinService
    {
        List<RelationshipToNextOfKinDTO> GetAll();
    }
}
