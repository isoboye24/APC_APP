using APC.Applications.DTO;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IGraphicalRepresentationService
    {
        List<GraphDTO> GetAllAnnualRaisedDues();
        List<GraphDTO> GetAllAnnualExpenditures();
    }
}
