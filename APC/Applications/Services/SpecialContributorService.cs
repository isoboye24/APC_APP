using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class SpecialContributorService : ISpecialContributorService
    {
        private readonly ISpecialContributorRepository _repository;
        private readonly ISpecialContributionRepository _specialContributionRepository;
        private readonly IMemberRepository _memberRepository;
        public SpecialContributorService(ISpecialContributorRepository repository, ISpecialContributionRepository specialContributionRepository,
            IMemberRepository memberRepository)
        {
            _repository = repository;
            _specialContributionRepository = specialContributionRepository;
            _memberRepository = memberRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(SpecialContributor data)
        {
            if (_repository.Exists(data.MemberId, data.SpecialContributionId))
                throw new Exception("Contributor already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<SpecialContributorDTO> GetAllByContributionId(int id)
        {
            var data = (from c in _repository.GetAllByContributionId(id)
                        join m in _memberRepository.GetAll() on c.memberID equals m.MemberId
                        join sc in _specialContributionRepository.GetAll() on c.specialContributionID equals sc.specialContributionID
                        select new SpecialContributorDTO
                        {
                            SpecialContributorId = c.specialContributorID,
                            MemberId = c.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            ImagePath = m.PersonalInfo.ImagePath,
                            AmountContributed = c.amountContributed,
                            FormattedAmountContributed = ("€ " + c.amountContributed).ToString(),
                            FormattedContributedDate = c.contributedDate.ToString("dd.MM.yyyy"),
                            ContributedDate = c.contributedDate,
                            AmountExpected = sc.amountExpected,
                            FormattedAmountExpected = ("€ " + sc.amountExpected).ToString(),
                            Balance = ("€ " + (sc.amountExpected - c.amountContributed)).ToString(),

                            PaymentStatus = c.amountContributed <= 0 ? "Not Paid" : (c.amountContributed > 0 && 
                            c.amountContributed < sc.amountExpected) ? "Not Completed" : c.amountContributed == sc.amountExpected ? 
                            "Completed" : ((c.amountContributed - sc.amountExpected) + " € Extra").ToString(),

                            Summary = sc.summary,
                            SpecialContributionId = sc.specialContributionID,
                        }).ToList();

            data = data.Select((x, index) =>
            {
                x.Counter = index + 1;
                return x;
            }).OrderByDescending(x => x.ContributedDate.Year).ThenByDescending(x => x.ContributedDate.Month).ThenByDescending(x => x.ContributedDate.Day).ThenBy(x => x.FirstName).ToList();

            return data;
        }

        public List<SpecialContributorDTO> GetAllDeletedSpecialContributors()
        {
            var data = (from c in _repository.GetAllDeletedContributors()
                        join m in _memberRepository.GetAll() on c.memberID equals m.MemberId
                        join sc in _specialContributionRepository.GetAll() on c.specialContributionID equals sc.specialContributionID
                        select new SpecialContributorDTO
                        {
                            SpecialContributorId = c.specialContributorID,
                            MemberId = c.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            ImagePath = m.PersonalInfo.ImagePath,
                            AmountContributed = c.amountContributed,
                            FormattedAmountContributed = ("€ " + c.amountContributed).ToString(),
                            FormattedContributedDate = c.contributedDate.ToString("dd.MM.yyyy"),
                            ContributedDate = c.contributedDate,
                            AmountExpected = sc.amountExpected,
                            FormattedAmountExpected = ("€ " + sc.amountExpected).ToString(),
                            Balance = (sc.amountExpected - c.amountContributed).ToString(),

                            PaymentStatus = c.amountContributed <= 0 ? "Not Paid" : (c.amountContributed > 0 &&
                            c.amountContributed < sc.amountExpected) ? "Not Completed" : c.amountContributed == sc.amountExpected ?
                            "Completed" : ((c.amountContributed - sc.amountExpected) + " € Extra").ToString(),

                            Summary = sc.summary,
                            SpecialContributionId = sc.specialContributionID,
                        }).ToList();

            data = data.Select((x, index) =>
            {
                x.Counter = index + 1;
                return x;
            }).OrderByDescending(x => x.ContributedDate.Year).ThenByDescending(x => x.ContributedDate.Month).ThenByDescending(x => x.ContributedDate.Day).ThenBy(x => x.FirstName).ToList();

            return data;
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);
        
        public decimal GetAmountContributedByContributionId(int id)
            => _repository.GetAmountContributedByContributionId(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(SpecialContributor data)
        {
            var contributor = _repository.GetById(data.SpecialContributorId);

            if (contributor == null)
                throw new InvalidOperationException("Contributor not found");

            data.UpdateMember(data.MemberId);
            data.UpdateAmountContributed(data.AmountContributed);
            data.UpdateContributedDate(data.ContributedDate);
            data.UpdateSummary(data.Summary);
            
            return _repository.Update(data);
        }
    }
}
