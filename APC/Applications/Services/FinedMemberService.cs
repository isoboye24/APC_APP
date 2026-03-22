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

        public List<FinedMemberDTO> GetAll()
        {
            var data = (from f in _repository.GetAll()
                        join m in _memberRepository.GetAll() on f.memberID equals m.MemberId
                        join g in _genderRepository.GetAll() on m.PersonalInfo.GenderId equals g.genderID
                        join p in _positionRepository.GetAll() on m.MembershipInfo.PositionId equals p.positionID
                        join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                        select new FinedMemberDTO
                        {

                            FinedMemberId = f.finedMemberID,
                            AmountPaid = (decimal)f.amountPaid,
                            FormattedAmountPaid = (f.amountPaid + " €").ToString(),
                            Summary = f.summary,
                            ConstitutionId = f.constitutionID,
                            Section = c.section,
                            ShortDescription = c.ShortDescription,
                            AmountExpected = (c.fine + " €").ToString(),
                            MemberId = f.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            ImagePath = m.PersonalInfo.ImagePath,
                            GenderId = m.PersonalInfo.GenderId,
                            GenderName = g.genderName,
                            PositionId = m.MembershipInfo.PositionId,
                            PositionName = p.positionName,
                            Status = f.amountPaid <= 0 ? "Not Paid" : (f.amountPaid > 0 && f.amountPaid < c.fine)  ? "Not Completed" : f.amountPaid == c.fine ? "Completed" : ((f.amountPaid - c.fine) + " € Extra").ToString(),
                            FineDate = f.fineDate,
                            FormattedFineDate = f.fineDate.ToString("dd.MM.yyyy"),
                        }).OrderByDescending(x => x.FineDate.Year).ThenByDescending(x => x.FineDate.Month).ThenByDescending(x => x.FineDate.Day).ThenBy(x => x.FirstName).ToList();

            return data;
        }

        public List<FinedMemberDTO> GetAllDeletedFinedMembers()
        {
            var data = (from f in _repository.GetAllDeletedFinedMembers()
                        join m in _memberRepository.GetAll() on f.memberID equals m.MemberId
                        join g in _genderRepository.GetAll() on m.PersonalInfo.GenderId equals g.genderID
                        join p in _positionRepository.GetAll() on m.MembershipInfo.PositionId equals p.positionID
                        join c in _constitutionRepository.GetAll() on f.constitutionID equals c.constitutionID
                        select new FinedMemberDTO
                        {

                            FinedMemberId = f.finedMemberID,
                            AmountPaid = (decimal)f.amountPaid,
                            FormattedAmountPaid = (f.amountPaid + " €").ToString(),
                            Summary = f.summary,
                            ConstitutionId = f.constitutionID,
                            Section = c.section,
                            ShortDescription = c.ShortDescription,
                            AmountExpected = (c.fine + " €").ToString(),
                            MemberId = f.memberID,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            ImagePath = m.PersonalInfo.ImagePath,
                            GenderId = m.PersonalInfo.GenderId,
                            GenderName = g.genderName,
                            PositionId = m.MembershipInfo.PositionId,
                            PositionName = p.positionName,
                            Status = f.amountPaid <= 0 ? "Not Paid" : (f.amountPaid > 0 && f.amountPaid < c.fine)  ? "Not Completed" : f.amountPaid == c.fine ? "Completed" : ((f.amountPaid - c.fine) + " € Extra").ToString(),
                            FineDate = f.fineDate,
                            FormattedFineDate = f.fineDate.ToString("dd.MM.yyyy"),
                        }).OrderByDescending(x => x.FineDate.Year).ThenByDescending(x => x.FineDate.Month).ThenByDescending(x => x.FineDate.Day).ThenBy(x => x.FirstName).ToList();

            return data;
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

            data.UpdateConstitution(data.ConstitutionId);
            data.UpdateAmountPaid(data.AmountPaid);
            data.UpdateMember(data.MemberId);
            data.UpdateSummary(data.Summary);
            data.UpdateFineDate(data.FineDate);

            return _repository.Update(data);
        }
    }
}
