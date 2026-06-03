using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
                .GroupBy(x => x.year)
                .Select(g => new GraphDTO
                {
                    Year = g.Key,
                    Amount = g.Sum(x => x.totalAmountSpent)
                })
                .OrderBy(x => x.Year)
                .ToList();
        }

        public List<GraphDTO> GetAllAnnualRaisedDues()
        {
            return _financialRepository.GetAll()
                .GroupBy(x => x.year)
                .Select(g => new GraphDTO
                {
                    Year = g.Key,
                    Amount = g.Sum(x => x.totalAmountRaised)
                })
                .OrderBy(x => x.Year)
                .ToList();
        }
    }
}
