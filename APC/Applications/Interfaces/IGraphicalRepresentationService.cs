using APC.Applications.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IGraphicalRepresentationService
    {
        List<GraphDTO> GetAllAnnualRaisedDues();
        List<GraphDTO> GetAllAnnualExpenditures();
    }
}
