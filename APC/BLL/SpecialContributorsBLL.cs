using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class SpecialContributorsBLL : IBLL<SpecialContributorDTO, SpecialContributorDetailDTO>
    {
        SpecialContributorsDAO dao = new SpecialContributorsDAO();
        public bool Delete(SpecialContributorDetailDTO entity)
        {
            SPECIAL_CONTRIBUTORS contributor = new SPECIAL_CONTRIBUTORS();
            contributor.specialContributorID = entity.ContributorID;

            return dao.Delete(contributor);
        }

        public bool GetBack(SpecialContributorDetailDTO entity)
        {
            return dao.GetBack(entity.ContributorID);
        }

        public bool Insert(SpecialContributorDetailDTO entity)
        {
            SPECIAL_CONTRIBUTORS contributor = new SPECIAL_CONTRIBUTORS();
            contributor.specialContributionID = entity.ContributionID;
            contributor.summary = entity.Surname;
            contributor.contributedDate = entity.ContributedDate;
            contributor.memberID = entity.MemberID;
            contributor.amountContributed = entity.AmountContributed;

            return dao.Insert(contributor);
        }

        public SpecialContributorDTO Select()
        {
            throw new NotImplementedException();
        }

        public SpecialContributorDTO Select(int contributionID)
        {
            SpecialContributorDTO dto = new SpecialContributorDTO();
            dto.SpecialContributors = dao.Select(contributionID);

            return dto;
        }

        public bool Update(SpecialContributorDetailDTO entity)
        {
            SPECIAL_CONTRIBUTORS contributor = new SPECIAL_CONTRIBUTORS();
            contributor.specialContributorID = entity.ContributorID;
            contributor.specialContributionID = entity.ContributionID;
            contributor.summary = entity.Surname;
            contributor.contributedDate = entity.ContributedDate;
            contributor.memberID = entity.MemberID;
            contributor.amountContributed = entity.AmountContributed;

            return dao.Update(contributor);
        }
    }
}
