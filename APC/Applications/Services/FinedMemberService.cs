using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class FinedMemberService : IFinedMemberService
    {
        private readonly IFinedMemberRepository _repository;
        public FinedMemberService(IFinedMemberRepository repository)
        {
            _repository = repository;
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

        public List<FinedMember> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(FinedMember data)
        {
            var check = _repository.GetById(data.FinedMemberId);
            if (check == null)
                throw new Exception("Fined member not found");

            data.UpdateConstitution(data.ConstitutionId);
            data.UpdateAmountPaid(data.AmountPaid);
            data.UpdateMember(data.MemberId);
            data.UpdateSummary(data.Summary);
            data.UpdateFineDate(data.FineDate);

            return _repository.Update(data);
        }
    }
}
