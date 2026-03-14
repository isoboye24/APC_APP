using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class SpecialContributionService : ISpecialContributionService
    {
        private readonly ISpecialContributionRepository _repository;
        public SpecialContributionService(ISpecialContributionRepository repository)
        {
            _repository = repository;
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

        public List<SpecialContribution> GetAll()
            => _repository.GetAll();

        public List<SpecialContribution> GetAllDeleted()
            => _repository.GetAllDeleted();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public List<SpecialContributionFullDetails> GetFullSpecialContributionDetails()
            => _repository.GetFullSpecialContributionDetails();

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
