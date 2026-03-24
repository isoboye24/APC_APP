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
                        select new SpecialContributionDTO
                        {
                            SpecialContributionId = sc.specialContributionID,
                            Title = sc.title,
                            Summary = sc.summary,
                            AmountToContribute = sc.amountToContribute,
                            SupervisorId = sc.supervisorID,
                            Supervisor = m.PersonalInfo.FirstName + " " + m.PersonalInfo.LastName,
                            ContributionStartDate = sc.contributionStartDate,
                            ContributionEndDate = sc.contributionEndDate,
                            AmountExpected = sc.amountExpected
                            Status = f.amountPaid <= 0 ? "Not Paid" : (f.amountPaid > 0 && f.amountPaid < c.fine) ? "Not Completed" : f.amountPaid == c.fine ? "Completed" : ((f.amountPaid - c.fine) + " € Extra").ToString(),
                            FineDate = f.fineDate,
                            FormattedFineDate = f.fineDate.ToString("dd.MM.yyyy"),
                        }).OrderByDescending(x => x.FineDate.Year).ThenByDescending(x => x.FineDate.Month).ThenByDescending(x => x.FineDate.Day).ThenBy(x => x.FirstName).ToList();

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

            contribution.UpdateTitle(data.Title);
            contribution.UpdateSummary(data.Summary);
            contribution.UpdateAmountToContribute(data.AmountToContribute);
            contribution.UpdateSupervisor(data.SupervisorId);
            contribution.UpdateContributionStartDate(data.ContributionStartDate);
            contribution.UpdateContributionEndDate(data.ContributionEndDate);
            contribution.UpdateAmountExpected(data.AmountExpected);

            return _repository.Update(contribution);
        }
    }
}
