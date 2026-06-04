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
                        join m in _memberRepository.GetAll()
                            on c.memberID equals m.memberID
                        join sc in _specialContributionRepository.GetAll()
                            on c.specialContributionID equals sc.specialContributionID
                        select new
                        {
                            c.specialContributorID,
                            c.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            c.amountContributed,
                            c.contributedDate,
                            sc.amountExpected,
                            sc.summary,
                            sc.specialContributionID,
                        })
                        .ToList();

            var result = data.Select(x => new SpecialContributorDTO
            {
                SpecialContributorId = x.specialContributorID,
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                AmountContributed = x.amountContributed,
                FormattedAmountContributed = "€ " + x.amountContributed,
                FormattedContributedDate = x.contributedDate.ToString("dd.MM.yyyy"),
                ContributedDate = x.contributedDate,

                AmountExpected = x.amountExpected,
                FormattedAmountExpected = "€ " + x.amountExpected,

                Balance = "€ " + (x.amountExpected - x.amountContributed),

                PaymentStatus =
                    x.amountContributed <= 0 ? "Not Paid" :
                    x.amountContributed < x.amountExpected ? "Not Completed" :
                    x.amountContributed == x.amountExpected ? "Completed" :
                    (x.amountContributed - x.amountExpected) + " € Extra",

                Summary = x.summary,
                SpecialContributionId = x.specialContributionID,
            })
            .OrderByDescending(x => x.ContributedDate)
            .ThenBy(x => x.FirstName)
            .ToList();

            result = result.Select((x, index) =>
            {
                x.Counter = index + 1;
                return x;
            }).ToList();

            return result;
        }

        public List<SpecialContributorDTO> GetAllDeletedSpecialContributors()
        {
            var data = (from c in _repository.GetAllDeletedContributors()
                        join m in _memberRepository.GetAll()
                            on c.memberID equals m.memberID
                        join sc in _specialContributionRepository.GetAll()
                            on c.specialContributionID equals sc.specialContributionID
                        select new
                        {
                            c.specialContributorID,
                            c.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            c.amountContributed,
                            c.contributedDate,
                            sc.amountExpected,
                            sc.summary,
                            sc.specialContributionID,
                        })
                        .ToList();

            var result = data.Select(x => new SpecialContributorDTO
            {
                SpecialContributorId = x.specialContributorID,
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                AmountContributed = x.amountContributed,
                FormattedAmountContributed = "€ " + x.amountContributed,
                FormattedContributedDate = x.contributedDate.ToString("dd.MM.yyyy"),
                ContributedDate = x.contributedDate,

                AmountExpected = x.amountExpected,
                FormattedAmountExpected = "€ " + x.amountExpected,

                Balance = "€ " + (x.amountExpected - x.amountContributed),

                PaymentStatus =
                    x.amountContributed <= 0 ? "Not Paid" :
                    x.amountContributed < x.amountExpected ? "Not Completed" :
                    x.amountContributed == x.amountExpected ? "Completed" :
                    (x.amountContributed - x.amountExpected) + " € Extra",

                Summary = x.summary,
                SpecialContributionId = x.specialContributionID,
            })
            .OrderByDescending(x => x.ContributedDate)
            .ThenBy(x => x.FirstName)
            .ToList();

            result = result.Select((x, index) =>
            {
                x.Counter = index + 1;
                return x;
            }).ToList();

            return result;
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

            else
            {
                return _repository.Update(data);                
            }
        }
    }
}
