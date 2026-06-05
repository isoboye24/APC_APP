using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class FinedMemberService : IFinedMemberService
    {
        private readonly IFinedMemberRepository _repository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IConstitutionRepository _constitutionRepository;
        public FinedMemberService(IFinedMemberRepository repository, IMemberRepository memberRepository, IGenderRepository genderRepository,
            IPositionRepository positionRepository, IConstitutionRepository constitutionRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _genderRepository = genderRepository;
            _positionRepository = positionRepository;
            _constitutionRepository = constitutionRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(FinedMember data)
        {
            if (_repository.Exists(data.ConstitutionId, data.MemberId, data.FineDate))
                throw new Exception("Fined member already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<FinedMemberDTO> GetAll()
        {
            var data = (from f in _repository.GetAll()
                        join m in _memberRepository.GetAll() on f.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                        select new
                        {
                            f.finedMemberID,
                            f.amountPaid,
                            Summary = f.summary,
                            ConstitutionId = f.constitutionID,
                            Section = c.section,
                            c.ShortDescription,
                            c.fine,
                            f.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            m.genderID,
                            g.genderName,
                            m.positionID,
                            p.positionName,
                            f.fineDate,
                        })
                        .ToList();

            return data.Select(x => new FinedMemberDTO
            {
                FinedMemberId = x.finedMemberID,
                AmountPaid = (decimal)x.amountPaid,
                FormattedAmountPaid = (x.amountPaid + " €").ToString(),
                Summary = x.Summary,
                ConstitutionId = x.ConstitutionId,
                Section = x.Section,
                ShortDescription = x.ShortDescription,
                AmountExpected = x.fine,
                FormattedAmountExpected = (x.fine + " €").ToString(),
                Balance = (x.fine - x.amountPaid).ToString(),
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                GenderId = x.genderID,
                GenderName = x.genderName,
                PositionId = x.positionID,
                PositionName = x.positionName,
                Status = x.amountPaid <= 0 ? "Not Paid" : (x.amountPaid > 0 && x.amountPaid < x.fine) ? "Not Completed" : x.amountPaid == x.fine ? "Completed" : ((x.amountPaid - x.fine) + " € Extra").ToString(),
                FineDate = x.fineDate,
                FormattedFineDate = x.fineDate.ToString("dd.MM.yyyy"),
            })
            .OrderByDescending(x => x.FineDate).ThenBy(x => x.FirstName)
            .ToList();
        }

        public List<FinedMemberDTO> GetAllDeletedFinedMembers()
        {
            var data = (from f in _repository.GetAllDeletedFinedMembers()
                        join m in _memberRepository.GetAll() on f.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                        select new
                        {
                            f.finedMemberID,
                            f.amountPaid,
                            Summary = f.summary,
                            ConstitutionId = f.constitutionID,
                            Section = c.section,
                            c.ShortDescription,
                            c.fine,
                            f.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            m.genderID,
                            g.genderName,
                            m.positionID,
                            p.positionName,
                            f.fineDate,
                        })
                        .ToList();

            return data.Select(x => new FinedMemberDTO
            {
                FinedMemberId = x.finedMemberID,
                AmountPaid = (decimal)x.amountPaid,
                FormattedAmountPaid = (x.amountPaid + " €").ToString(),
                Summary = x.Summary,
                ConstitutionId = x.ConstitutionId,
                Section = x.Section,
                ShortDescription = x.ShortDescription,
                AmountExpected = x.fine,
                FormattedAmountExpected = (x.fine + " €").ToString(),
                Balance = (x.fine - x.amountPaid).ToString(),
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                GenderId = x.genderID,
                GenderName = x.genderName,
                PositionId = x.positionID,
                PositionName = x.positionName,
                Status = x.amountPaid <= 0 ? "Not Paid" : (x.amountPaid > 0 && x.amountPaid < x.fine) ? "Not Completed" : x.amountPaid == x.fine ? "Completed" : ((x.amountPaid - x.fine) + " € Extra").ToString(),
                FineDate = x.fineDate,
                FormattedFineDate = x.fineDate.ToString("dd.MM.yyyy"),
            })
            .OrderByDescending(x => x.FineDate).ThenBy(x => x.FirstName)
            .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(FinedMember data)
        {
            var check = _repository.GetById(data.FinedMemberId);
            if (check == null)
                throw new Exception("Fined member not found");

            else
            {
                return _repository.Update(data);                
            }
        }


        public decimal GetTotalPaidFines()
        {
            return _repository.GetAll().Sum(x => x.amountPaid ?? 0);
        }
        
        public decimal GetTotalFinesExpected()
        {
            var totalExpectedAmount = (from fm in _repository.GetAll()
                                       join c in _constitutionRepository.GetAll()
                                           on fm.constitutionID equals c.constitutionID
                                       select c.fine).Sum();

            return totalExpectedAmount;
        }
       
        public decimal GetTotalFinesPaidByMember(int memberId)
        {
            return _repository.GetAll().Where(x =>x.memberID == memberId).Sum(x => x.amountPaid ?? 0);
        }

        public decimal GetTotalFinesExpectedByMember(int memberId)
        {
            var totalExpectedAmount = (from fm in _repository.GetAll().Where(x => x.memberID == memberId)
                                       join c in _constitutionRepository.GetAll()
                                           on fm.constitutionID equals c.constitutionID
                                       select c.fine).Sum();

            return totalExpectedAmount;
        }

        public int AnnualFinesCountById(int memberId, int year)
            => _repository.GetAll().Count(x => x.memberID == memberId && x.fineDate.Year == year);

        public int TotalFinesCountById(int memberId)
            => _repository.GetAll().Count(x => x.memberID == memberId);

        public List<FinedMemberDTO> GetAllFineListsById(int memberId)
        {
            var data = (from f in _repository.GetAll().Where(x => x.memberID == memberId)
                        join m in _memberRepository.GetAll() on f.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                        select new
                        {
                            f.finedMemberID,
                            f.amountPaid,
                            Summary = f.summary,
                            ConstitutionId = f.constitutionID,
                            Section = c.section,
                            c.ShortDescription,
                            c.fine,
                            f.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            m.genderID,
                            g.genderName,
                            m.positionID,
                            p.positionName,
                            f.fineDate,
                        })
                        .ToList();

            return data.Select(x => new FinedMemberDTO
            {
                FinedMemberId = x.finedMemberID,
                AmountPaid = (decimal)x.amountPaid,
                FormattedAmountPaid = (x.amountPaid + " €").ToString(),
                Summary = x.Summary,
                ConstitutionId = x.ConstitutionId,
                Section = x.Section,
                ShortDescription = x.ShortDescription,
                AmountExpected = x.fine,
                FormattedAmountExpected = (x.fine + " €").ToString(),
                Balance = (x.fine - x.amountPaid).ToString(),
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                GenderId = x.genderID,
                GenderName = x.genderName,
                PositionId = x.positionID,
                PositionName = x.positionName,
                Status = x.amountPaid <= 0 ? "Not Paid" : (x.amountPaid > 0 && x.amountPaid < x.fine) ? "Not Completed" : x.amountPaid == x.fine ? "Completed" : ((x.amountPaid - x.fine) + " € Extra").ToString(),
                FineDate = x.fineDate,
                FormattedFineDate = x.fineDate.ToString("dd.MM.yyyy"),
            })
            .OrderByDescending(x => x.FineDate).ThenByDescending(x => x.AmountExpected)
            .ToList();
        }

        public List<FinedMemberDTO> GetAnnualFineListsById(int memberId, int year)
        {
            var data = (from f in _repository.GetAll().Where(x => x.memberID == memberId && x.year == year)
                        join m in _memberRepository.GetAll() on f.memberID equals m.memberID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                        select new
                        {
                            f.finedMemberID,
                            f.amountPaid,
                            Summary = f.summary,
                            ConstitutionId = f.constitutionID,
                            Section = c.section,
                            c.ShortDescription,
                            c.fine,
                            f.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            m.genderID,
                            g.genderName,
                            m.positionID,
                            p.positionName,
                            f.fineDate,
                        })
                        .ToList();

            return data.Select(x => new FinedMemberDTO
            {
                FinedMemberId = x.finedMemberID,
                AmountPaid = (decimal)x.amountPaid,
                FormattedAmountPaid = (x.amountPaid + " €").ToString(),
                Summary = x.Summary,
                ConstitutionId = x.ConstitutionId,
                Section = x.Section,
                ShortDescription = x.ShortDescription,
                AmountExpected = x.fine,
                FormattedAmountExpected = (x.fine + " €").ToString(),
                Balance = (x.fine - x.amountPaid).ToString(),
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                GenderId = x.genderID,
                GenderName = x.genderName,
                PositionId = x.positionID,
                PositionName = x.positionName,
                Status = x.amountPaid <= 0 ? "Not Paid" : (x.amountPaid > 0 && x.amountPaid < x.fine) ? "Not Completed" : x.amountPaid == x.fine ? "Completed" : ((x.amountPaid - x.fine) + " € Extra").ToString(),
                FineDate = x.fineDate,
                FormattedFineDate = x.fineDate.ToString("dd.MM.yyyy"),
            })
            .OrderByDescending(x => x.FineDate).ThenByDescending(x => x.AmountExpected)
            .ToList();
        }
    }
}
