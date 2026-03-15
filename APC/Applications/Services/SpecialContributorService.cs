using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class SpecialContributorService : ISpecialContributorService
    {
        private readonly ISpecialContributorRepository _repository;
        public SpecialContributorService(ISpecialContributorRepository repository)
        {
            _repository = repository;
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

        public List<SpecialContributor> GetAll()
            => _repository.GetAll();

        public List<SpecialContributor> GetAllDeleted()
            => _repository.GetAllDeleted();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public List<SpecialContributorFullDetails> GetFullSpecialContributorFullDetails()
            => _repository.GetFullSpecialContributorDetails();

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(SpecialContributor data)
        {
            var contributor = _repository.GetById(data.SpecialContributorId);

            if (contributor == null)
                throw new InvalidOperationException("Contributor not found");

            contributor.UpdateMember(data.MemberId);
            contributor.UpdateAmountContributed(data.AmountContributed);
            contributor.UpdateContributedDate(data.ContributedDate);
            contributor.UpdateSummary(data.Summary);
            
            return _repository.Update(contributor);
        }
    }
}
