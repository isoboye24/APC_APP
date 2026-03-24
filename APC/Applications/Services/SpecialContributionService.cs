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
        public SpecialContributionService(ISpecialContributionRepository repository, IMemberRepository memberRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
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
                        join m in _memberRepository.GetAll() on sc.supervisorID equals m.MemberId
                        let totalContributedAmount = _specialContributorRepository.GetByAmountContributedByContributionId(sc.specialContributionID)
                        select new SpecialContributionDTO
                        {
                            SpecialContributionId = sc.specialContributionID,
                            Title = sc.title,
                            Summary = sc.summary,
                            AmountToContribute = sc.amountToContribute,
                            SupervisorId = sc.supervisorID,
                            Supervisor = m.PersonalInfo.FirstName + " " + m.PersonalInfo.LastName,
                            ContributionStartDate = sc.contributionStartDate,
                            FormattedContributionStartDate = sc.contributionStartDate.ToString("dd.MM.yyyy"),
                            ContributionEndDate = sc.contributionEndDate,
                            FormattedContributionEndDate = sc.contributionEndDate.ToString("dd.MM.yyyy"),
                            AmountExpected = sc.amountExpected,
                            Status = totalContributedAmount <= 0 ? "Not Paid" : (totalContributedAmount > 0 && totalContributedAmount < sc.amountExpected) ? "Not Completed" : totalContributedAmount == sc.amountExpected ? "Completed" : ((totalContributedAmount - sc.amountExpected) + " € Extra").ToString(),
                            TotalContributedAmount = totalContributedAmount,

                        }).OrderByDescending(x => x.ContributionStartDate.Year).ThenByDescending(x => x.ContributionStartDate.Month).ThenByDescending(x => x.ContributionStartDate.Day).ThenBy(x => x.Title).ToList();

            return data;
        }
        
        public List<SpecialContributionDTO> GetAllDeletedSpecialContributions()
        {
            var data = (from sc in _repository.GetAllDeletedSpecialContributions()
                        join m in _memberRepository.GetAll() on sc.supervisorID equals m.MemberId
                        let totalContributedAmount = _specialContributorRepository.GetByAmountContributedByContributionId(sc.specialContributionID)
                        select new SpecialContributionDTO
                        {
                            SpecialContributionId = sc.specialContributionID,
                            Title = sc.title,
                            Summary = sc.summary,
                            AmountToContribute = sc.amountToContribute,
                            SupervisorId = sc.supervisorID,
                            Supervisor = m.PersonalInfo.FirstName + " " + m.PersonalInfo.LastName,
                            ContributionStartDate = sc.contributionStartDate,
                            FormattedContributionStartDate = sc.contributionStartDate.ToString("dd.MM.yyyy"),
                            ContributionEndDate = sc.contributionEndDate,
                            FormattedContributionEndDate = sc.contributionEndDate.ToString("dd.MM.yyyy"),
                            AmountExpected = sc.amountExpected,
                            Status = totalContributedAmount <= 0 ? "Not Paid" : (totalContributedAmount > 0 && totalContributedAmount < sc.amountExpected) ? "Not Completed" : totalContributedAmount == sc.amountExpected ? "Completed" : ((totalContributedAmount - sc.amountExpected) + " € Extra").ToString(),
                            TotalContributedAmount = totalContributedAmount,

                        }).OrderByDescending(x => x.ContributionStartDate.Year).ThenByDescending(x => x.ContributionStartDate.Month).ThenByDescending(x => x.ContributionStartDate.Day).ThenBy(x => x.Title).ToList();

            return data;
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
