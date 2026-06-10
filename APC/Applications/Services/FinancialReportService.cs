using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace APC.Applications.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _repository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGeneralMeetingRepository _generalMeetingRepository;

        private readonly IGeneralMeetingAttendanceRepository _generalMeetingAttendanceRepository;
        private readonly IFinedMemberRepository _finedMemberRepository;
        private readonly IEventSalesRepository _eventSalesRepository;

        private readonly IEventExpenditureRepository _eventExpenditureRepository;
        private readonly IExpenditureRepository _expenditureRepository;

        public FinancialReportService(IFinancialReportRepository repository, IGeneralMeetingAttendanceRepository generalMeetingAttendanceRepository, 
            IGeneralMeetingRepository generalMeetingRepository, IMemberRepository memberRepository, IFinedMemberRepository finedMemberRepository,
            IEventSalesRepository eventSalesRepository, IEventExpenditureRepository eventExpenditureRepository, IExpenditureRepository expenditureRepository)
        {
            _repository = repository;
            _generalMeetingAttendanceRepository = generalMeetingAttendanceRepository;
            _generalMeetingRepository = generalMeetingRepository;
            _memberRepository = memberRepository;
            _eventSalesRepository = eventSalesRepository;
            _finedMemberRepository = finedMemberRepository;
            _eventExpenditureRepository = eventExpenditureRepository;
            _expenditureRepository = expenditureRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(FinancialReport data)
        {
            if (_repository.Exists(data.Year))
                throw new Exception("Financial Report already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public decimal CalculateTotalAmountRaisedAnnually(int year)
        {
            decimal totalDuesRaised = _generalMeetingAttendanceRepository.GetAll()
                                    .Where(x => x.year == year)
                                    .Sum(x => (decimal?)x.monthlyDues) ?? 0;

            decimal totalFinesPaid = _finedMemberRepository.GetAll()
                                    .Where(x => x.year == year)
                                    .Sum(x => (decimal?)x.amountPaid) ?? 0;

            decimal totalEventSales = _eventSalesRepository.GetAll()
                                    .Where(x => x.year == year)
                                    .Sum(x => (decimal?)x.amountSold) ?? 0;

            return totalDuesRaised + totalFinesPaid + totalEventSales;
        }
        
        public decimal CalculateTotalAmountSpentAnnually(int year)
        {
            decimal totalExpenditure = _expenditureRepository.GetAll()
                                    .Where(x => x.year == year)
                                    .Sum(x => (decimal?)x.amountSpent) ?? 0;

            decimal totalEventExpenditure = _eventExpenditureRepository.GetAll()
                                    .Where(x => x.year == year)
                                    .Sum(x => (decimal?)x.amountSpent) ?? 0;

            return totalExpenditure + totalEventExpenditure;
        }

        public List<FinancialReportDTO> GetAll()
        {
            return _repository.GetAll()
                .ToList()
                 .Select(x => new FinancialReportDTO
                 {
                     FinancialReportId = x.financialReportID,
                     TotalAmountRaised = CalculateTotalAmountRaisedAnnually(x.year),
                     FormattedTotalAmountRaised = CalculateTotalAmountRaisedAnnually(x.year) + " €",
                     TotalAmountSpent = CalculateTotalAmountSpentAnnually(x.year),
                     FormattedTotalAmountSpent = CalculateTotalAmountSpentAnnually(x.year) + " €",
                     Year = x.year,
                     Summary = x.summary,
                 })
                 .OrderByDescending(x => x.Year)
                 .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(FinancialReport data)
        {
            var check = _repository.GetById(data.FinancialReportId);
            if (check == null)
                throw new Exception("Financial Report not found");

            else
            {
                return _repository.Update(data);                
            }
        }

        public List<YearDTO> GetFinancialReportYearsOnly()
        {
            return _repository.GetAll()
                    .ToList()
                    .Select(x => x.year)
                    .Distinct()
                    .OrderByDescending(x => x)
                    .Select(x => new YearDTO
                    {
                        YearInValue = x,
                        YearInText = x.ToString(),
                    })
                    .ToList();
        }


        public decimal GetAmountContributedByMember(int ID)
        {
            return _generalMeetingAttendanceRepository.GetAll()
                   .Where(x => x.memberID == ID &&
                               !x.isDeleted &&
                               x.attendanceStatusID == 2 &&
                               x.monthlyDues > 0)
                   .Sum(x => (decimal?)x.monthlyDues) ?? 0;
        }

        public decimal GetAmountExpectedByMember(int ID)
        {
            var memberInfo = _memberRepository.GetById(ID);

            if (memberInfo == null || memberInfo.membershipDate == null)
                return 0;

            DateTime membershipDate = memberInfo.membershipDate.Value;

            int meetingCount = (from a in _generalMeetingAttendanceRepository.GetAll()
                                join g in _generalMeetingRepository.GetAll()
                                    on a.generalAttendanceID equals g.generalAttendanceID
                                where a.memberID == ID &&
                                      !g.isDeleted &&
                                      g.attendanceDate > membershipDate
                                select a)
                                .Count();

            decimal feePerMeeting = 10.0m;

            return meetingCount * feePerMeeting;
        }

        public decimal GetOverallTotalDues()
        {
            return _repository.GetAll()
                .Sum(x => (decimal?)x.totalAmountRaised) ?? 0;
        }

        public decimal GetTotalDuesByMonth(int month, int year)
        {
            return _generalMeetingAttendanceRepository.GetAll()
                   .Where(x => x.monthID == month 
                   && x.year == year 
                   && x.monthlyDues > 0)
                   .Sum(x => (decimal?)x.monthlyDues) ?? 0;
        }
        
        public decimal GetTotalDuesByYear(int year)
        {
            return _generalMeetingAttendanceRepository.GetAll()
                   .Where(x => x.year == year 
                   && x.monthlyDues > 0)
                   .Sum(x => (decimal?)x.monthlyDues) ?? 0;
        }

        public decimal GetOverallExpenditures()
        {
            return _repository.GetAll()
                .Sum(x => (decimal?)x.totalAmountSpent) ?? 0;
        }
        
        public decimal GetOverallExpendituresByYear(int year)
        {
            return _repository.GetAll()
                .Where(x => x.year == year)
                .Sum(x => (decimal?)x.totalAmountSpent) ?? 0;
        }
        
        public bool IsFinancialReportExisting(int year)
        {
            var totalAmountSpent = _repository.GetAll()
                .Where(x => x.year == year)
                .Sum(x => (decimal?)x.totalAmountSpent) ?? 0;

            var totalAmountDues = _repository.GetAll()
                .Where(x => x.year == year)
                .Sum(x => (decimal?)x.totalAmountRaised) ?? 0;

            if (totalAmountSpent > 0 || totalAmountDues > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal GetTotalDuesById(int memberId)
        {
            return _generalMeetingAttendanceRepository
                    .GetAll()
                    .Where(x => x.memberID == memberId)
                    .Sum(x => (decimal?)x.monthlyDues) ?? 0;
        }

        public decimal GetTotalAnnualDuesById(int memberId, int year)
        {
            return _generalMeetingAttendanceRepository
                    .GetAll()
                    .Where(x => x.memberID == memberId && x.year == year)
                    .Sum(x => (decimal?)x.monthlyDues) ?? 0;
        }

        public decimal GetTotalDuesExpectedById(int memberId)
        {
            int noOfAttendance = _generalMeetingAttendanceRepository
                    .GetAll()
                    .Where(x => x.memberID == memberId)
                    .Count();

            return (noOfAttendance * 10);
        }

        public decimal GetTotalAnnualDuesExpectedById(int memberId, int year)
        {
            int noOfAttendance = _generalMeetingAttendanceRepository
                    .GetAll()
                    .Where(x => x.memberID == memberId && x.year == year)
                    .Count();

            return (noOfAttendance * 10);
        }
    }
}
