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
    public class SpecialContributionsBLL : IBLL<SpecialContributionDTO, SpecialContributionDetailDTO>
    {
        SpecialContributionsDAO dao = new SpecialContributionsDAO();
        MonthDAO monthDAO = new MonthDAO(); 
        public bool Delete(SpecialContributionDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(SpecialContributionDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SpecialContributionDetailDTO entity)
        {
            SPECIAL_CONTRIBUTIONS specialContribution = new SPECIAL_CONTRIBUTIONS();
            specialContribution.title = entity.ContributionTitle;
            specialContribution.summary = entity.Summary;
            specialContribution.amountExpected = entity.AmountExpected;
            specialContribution.amountToContribute = entity.AmountToContribute;
            specialContribution.supervisorID = entity.SupervisorID;
            specialContribution.contributionStartDate = entity.ContributionStartDate;
            specialContribution.contributionEndDate = entity.ContributionEndDate;

            return dao.Insert(specialContribution);
        }

        public SpecialContributionDTO Select()
        {
            SpecialContributionDTO dto = new SpecialContributionDTO();
            dto.SpecialContributions = dao.Select();
            dto.Months = monthDAO.Select();

            return dto;
        }

        public bool Update(SpecialContributionDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
