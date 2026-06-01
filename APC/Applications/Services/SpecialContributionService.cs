using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class SpecialContributionService : ISpecialContributionService
    {
        private readonly ISpecialContributionRepository _repository;
        private readonly IMemberRepository _memberRepository;
        private readonly ISpecialContributorRepository _specialContributorRepository;
        public SpecialContributionService(ISpecialContributionRepository repository, IMemberRepository memberRepository,
            ISpecialContributorRepository specialContributorRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _specialContributorRepository = specialContributorRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(SpecialContribution data)
        {
            if (_repository.Exists(data.Title))
                throw new Exception("Contribution already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<SpecialContributionDTO> GetAll()
        {
            var data = (from sc in _repository.GetAll()
                        join m in _memberRepository.GetAll()
                            on sc.supervisorID equals m.memberID
                        select new
                        {
                            sc.specialContributionID,
                            sc.title,
                            sc.summary,
                            sc.amountToContribute,
                            sc.supervisorID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            sc.contributionStartDate,
                            sc.contributionEndDate,
                            sc.amountExpected,
                        })
                        .ToList();

            return data.Select(x =>
            {
                decimal totalContributedAmount =
                    _specialContributorRepository.GetAmountContributedByContributionId(
                        x.specialContributionID);

                return new SpecialContributionDTO
                {
                    SpecialContributionId = x.specialContributionID,
                    Title = x.title,
                    Summary = x.summary,
                    AmountToContribute = x.amountToContribute,
                    SupervisorId = x.supervisorID,
                    FirstName = x.name,
                    LastName = x.surname,
                    ImagePath = x.imagePath,

                    ContributionStartDate = x.contributionStartDate,
                    FormattedContributionStartDate =
                        x.contributionStartDate.ToString("dd.MM.yyyy"),

                    ContributionEndDate = x.contributionEndDate,
                    FormattedContributionEndDate =
                        x.contributionEndDate.ToString("dd.MM.yyyy"),

                    AmountExpected = x.amountExpected,

                    Status =
                        totalContributedAmount <= 0
                            ? "Not Paid"
                            : totalContributedAmount < x.amountExpected
                                ? "Not Completed"
                                : totalContributedAmount == x.amountExpected
                                    ? "Completed"
                                    : $"{totalContributedAmount - x.amountExpected} € Extra",

                    TotalContributedAmount = totalContributedAmount
                };
            })
            .OrderByDescending(x => x.ContributionStartDate)
            .ThenBy(x => x.LastName)
            .ToList();
        }

        public List<SpecialContributionDTO> GetAllDeletedSpecialContributions()
        {
            var data = (from sc in _repository.GetAllDeletedSpecialContributions()
                        join m in _memberRepository.GetAll()
                            on sc.supervisorID equals m.memberID
                        select new
                        {
                            sc.specialContributionID,
                            sc.title,
                            sc.summary,
                            sc.amountToContribute,
                            sc.supervisorID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            sc.contributionStartDate,
                            sc.contributionEndDate,
                            sc.amountExpected,
                        })
                        .ToList();

            return data.Select(x =>
            {
                decimal totalContributedAmount =
                    _specialContributorRepository.GetAmountContributedByContributionId(
                        x.specialContributionID);

                return new SpecialContributionDTO
                {
                    SpecialContributionId = x.specialContributionID,
                    Title = x.title,
                    Summary = x.summary,
                    AmountToContribute = x.amountToContribute,
                    SupervisorId = x.supervisorID,
                    FirstName = x.name,
                    LastName = x.surname,
                    ImagePath = x.imagePath,

                    ContributionStartDate = x.contributionStartDate,
                    FormattedContributionStartDate =
                        x.contributionStartDate.ToString("dd.MM.yyyy"),

                    ContributionEndDate = x.contributionEndDate,
                    FormattedContributionEndDate =
                        x.contributionEndDate.ToString("dd.MM.yyyy"),

                    AmountExpected = x.amountExpected,

                    Status =
                        totalContributedAmount <= 0
                            ? "Not Paid"
                            : totalContributedAmount < x.amountExpected
                                ? "Not Completed"
                                : totalContributedAmount == x.amountExpected
                                    ? "Completed"
                                    : $"{totalContributedAmount - x.amountExpected} € Extra",

                    TotalContributedAmount = totalContributedAmount
                };
            })
            .OrderByDescending(x => x.ContributionStartDate)
            .ThenBy(x => x.LastName)
            .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(SpecialContribution data)
        {
            var contribution = _repository.GetById(data.SpecialContributionId);

            if (contribution == null)
                throw new InvalidOperationException("Contribution not found");

            data.UpdateTitle(data.Title);
            data.UpdateSummary(data.Summary);
            data.UpdateAmountToContribute(data.AmountToContribute);
            data.UpdateSupervisor(data.SupervisorId);
            data.UpdateContributionStartDate(data.ContributionStartDate);
            data.UpdateContributionEndDate(data.ContributionEndDate);
            data.UpdateAmountExpected(data.AmountExpected);

            return _repository.Update(data);
        }
    }
}
