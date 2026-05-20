using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class GraphicalRepresentationService : IGraphicalRepresentationService
    {
        private readonly IFinancialReportRepository _financialRepository;
        public GraphicalRepresentationService(IFinancialReportRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public List<GraphDTO> GetAllAnnualExpenditures()
        {
            return _financialRepository.GetAll()
                 .Select(x => new GraphDTO
                 {
                     Amount = x.totalAmountSpent,
                     Year = x.year,
                 })
                 .OrderBy(x => x.Year)
                 .ToList();
        }

        public List<GraphDTO> GetAllAnnualRaisedDues()
        {
            return _financialRepository.GetAll()
                 .Select(x => new GraphDTO
                 {
                     Amount = x.totalAmountRaised,
                     Year = x.year,
                 })
                 .OrderBy(x => x.Year)
                 .ToList();
        }
    }
}
