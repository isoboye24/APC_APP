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
        MemberDAO memberDAO = new MemberDAO();
        public bool Delete(SpecialContributionDetailDTO entity)
        {
            SPECIAL_CONTRIBUTIONS contribution = new SPECIAL_CONTRIBUTIONS();
            contribution.specialContributionID = entity.SpecialContributionID;

            return dao.Delete(contribution);
        }

        public bool GetBack(SpecialContributionDetailDTO entity)
        {
            return dao.GetBack(entity.SpecialContributionID);
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
            dto.Members = memberDAO.Select();

            return dto;
        }

        public decimal OverallTotalContributions()
        {
            return dao.OverallTotalContributions();
        }

        public bool Update(SpecialContributionDetailDTO entity)
        {
            SPECIAL_CONTRIBUTIONS specialContribution = new SPECIAL_CONTRIBUTIONS();
            specialContribution.specialContributionID = entity.SpecialContributionID;
            specialContribution.title = entity.ContributionTitle;
            specialContribution.summary = entity.Summary;
            specialContribution.amountExpected = entity.AmountExpected;
            specialContribution.amountToContribute = entity.AmountToContribute;
            specialContribution.supervisorID = entity.SupervisorID;
            specialContribution.contributionStartDate = entity.ContributionStartDate;
            specialContribution.contributionEndDate = entity.ContributionEndDate;

            return dao.Update(specialContribution);
        }
    }
}
