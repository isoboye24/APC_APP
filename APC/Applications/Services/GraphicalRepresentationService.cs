using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class GraphicalRepresentationService : IGraphicalRepresentationService
    {
        private readonly IGeneralMeetingRepository _generalMeetingRepository;

        private readonly IGeneralMeetingAttendanceRepository _generalMeetingAttendanceRepository;
        private readonly IFinedMemberRepository _finedMemberRepository;
        private readonly IEventSalesRepository _eventSalesRepository;

        private readonly IEventExpenditureRepository _eventExpenditureRepository;
        private readonly IEventsRepository _eventsRepository;
        private readonly IExpenditureRepository _expenditureRepository;

        public GraphicalRepresentationService(IGeneralMeetingAttendanceRepository generalMeetingAttendanceRepository,
            IGeneralMeetingRepository generalMeetingRepository, IFinedMemberRepository finedMemberRepository,
            IEventSalesRepository eventSalesRepository, IEventExpenditureRepository eventExpenditureRepository, IEventsRepository eventsRepository,
        IExpenditureRepository expenditureRepository)
        {
            _generalMeetingRepository = generalMeetingRepository;
            _eventSalesRepository = eventSalesRepository;
            _finedMemberRepository = finedMemberRepository;
            _eventExpenditureRepository = eventExpenditureRepository;
            _eventsRepository = eventsRepository;
            _expenditureRepository = expenditureRepository;
            _generalMeetingAttendanceRepository = generalMeetingAttendanceRepository;
        }

        public List<GraphDTO> GetAllAnnualExpenditures()
        {
            var expenditures = _expenditureRepository.GetAll()
                                .GroupBy(x => x.expenditureDate.Year)
                                .Select(g => new
                                {
                                    Year = g.Key,
                                    Amount = g.Sum(x => x.amountSpent)
                                });

            var eventExpenditures =
                from expense in _eventExpenditureRepository.GetAll()
                join ev in _eventsRepository.GetAll()
                    on expense.eventID equals ev.eventID
                group expense.amountSpent by ev.eventDate.Year into g
                select new
                {
                    Year = g.Key,
                    Amount = g.Sum()
                };

            return expenditures
                .Concat(eventExpenditures)
                .GroupBy(x => x.Year)
                .Select(g => new GraphDTO
                {
                    Year = g.Key,
                    Amount = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Year)
                .ToList();
        }

        public List<GraphDTO> GetAllAnnualRaisedDues()
        {
            var dues =
                from attendance in _generalMeetingAttendanceRepository.GetAll()
                join meeting in _generalMeetingRepository.GetAll()
                    on attendance.generalAttendanceID equals meeting.generalAttendanceID
                group attendance.monthlyDues by meeting.attendanceDate.Year into g
                select new
                {
                    Year = g.Key,
                    Amount = (decimal)g.Sum()
                };

            var fines = _finedMemberRepository.GetAll()
                .GroupBy(x => x.fineDate.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Amount = (decimal)g.Sum(x => x.amountPaid)
                });

            var eventSales =
                from sale in _eventSalesRepository.GetAll()
                join ev in _eventsRepository.GetAll()
                    on sale.eventID equals ev.eventID
                group sale.amountSold by ev.eventDate.Year into g
                select new
                {
                    Year = g.Key,
                    Amount = (decimal)g.Sum()
                };

            return dues
                .Concat(fines)
                .Concat(eventSales)
                .GroupBy(x => x.Year)
                .Select(g => new GraphDTO
                {
                    Year = g.Key,
                    Amount = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Year)
                .ToList();
        }
    }
}
