using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class SpecialContributionsDAO : APCContexts, IDAO<SpecialContributionDetailDTO, SPECIAL_CONTRIBUTIONS>
    {
        public bool Delete(SPECIAL_CONTRIBUTIONS entity)
        {
            try
            {
                SPECIAL_CONTRIBUTIONS contribution = db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == entity.specialContributionID);
                contribution.isDeleted = true;
                contribution.deletedDate = DateTime.Today;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                SPECIAL_CONTRIBUTIONS contribution = db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == ID);
                contribution.isDeleted = false;
                contribution.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(SPECIAL_CONTRIBUTIONS entity)
        {
            try
            {
                db.SPECIAL_CONTRIBUTIONS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public decimal OverallTotalContributions()
        {
            try
            {
                List<decimal> totalContributions = new List<decimal>();
                var totalContribution = db.SPECIAL_CONTRIBUTIONS.Where(x => x.isDeleted == false).ToList();
                foreach (var item in totalContribution)
                {
                    var contributorsList = db.SPECIAL_CONTRIBUTORS.Where(x => x.isDeleted == false && x.specialContributionID == item.specialContributionID).ToList();
                    foreach (var contributor in contributorsList)
                    {
                        totalContributions.Add(contributor.amountContributed);
                    }
                }
                decimal total = totalContributions.Sum();

                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialContributionDetailDTO> Select()
        {
            try
            {
                List<SpecialContributionDetailDTO> specialContributions = new List<SpecialContributionDetailDTO>();
                int counter = 0;

                var list = (from s in db.SPECIAL_CONTRIBUTIONS.Where(x => x.isDeleted == false)
                            join m in db.MEMBER on s.supervisorID equals m.memberID
                            select new
                            {
                                specialContributionID = s.specialContributionID,
                                title = s.title,
                                summary = s.summary,
                                amountEach = s.amountToContribute,
                                amountExpected = s.amountExpected,
                                supervisorID = m.memberID,
                                supervisorName = m.name,
                                supervisorSurname = m.surname,
                                imagePath = m.imagePath,
                                contributionStartDate = s.contributionStartDate,
                                contributionEndDate = s.contributionEndDate,
                            }).OrderByDescending(x => x.contributionStartDate.Year)
                            .ThenByDescending(x => x.contributionStartDate.Month)
                            .ThenByDescending(x => x.contributionStartDate.Day)
                            .ThenBy(x => x.title).ToList();

                foreach (var item in list)
                {
                    SpecialContributionDetailDTO dto = new SpecialContributionDetailDTO();
                    List<decimal> amountsContributed = new List<decimal>();

                    dto.Counter = ++counter;
                    dto.SpecialContributionID = item.specialContributionID;
                    dto.ContributionTitle = item.title;
                    dto.Summary = item.summary;
                    dto.AmountToContribute = item.amountEach;
                    dto.AmountToContributeWithCurrency = item.amountEach + " €";
                    dto.AmountExpected = item.amountExpected;
                    dto.AmountExpectedWithCurrency = item.amountExpected + " €";

                    int contributorsCount = db.SPECIAL_CONTRIBUTORS.Count(x => x.isDeleted == false && x.specialContributionID == item.specialContributionID);
                    var contributors = db.SPECIAL_CONTRIBUTORS.Where(x => x.isDeleted == false && x.specialContributionID == item.specialContributionID).ToList();
                    foreach (var contributor in contributors)
                    {
                        if (contributor.amountContributed > 0)
                        {
                            amountsContributed.Add(contributor.amountContributed);
                        }
                        else
                        {
                            amountsContributed.Add(0);
                        }
                    }
                    int totalContributors = contributorsCount;
                    decimal totalAmountContributed = amountsContributed.Sum();

                    dto.Members = totalContributors;
                    dto.AmountContributed = totalAmountContributed;
                    dto.AmountContributedWithCurrency = totalAmountContributed + " €";
                    if (totalAmountContributed < item.amountExpected)
                    {
                        dto.Status = "Incomplete";
                    }
                    else if (totalAmountContributed == item.amountExpected)
                    {
                        dto.Status = "Completed";
                    }
                    else
                    {
                        dto.Status = "Excess";
                    }

                    dto.SupervisorID = item.supervisorID;
                    dto.SupervisorName = item.supervisorName;
                    dto.SupervisorSurname = item.supervisorSurname;
                    dto.ContributionStartDate = item.contributionStartDate;
                    dto.StartDate = item.contributionStartDate.Day + "." + item.contributionStartDate.Month + "." + item.contributionStartDate.Year;
                    dto.ContributionEndDate = item.contributionEndDate;
                    dto.EndDate = item.contributionEndDate.Day + "." + item.contributionEndDate.Month + "." + item.contributionEndDate.Year;
                    dto.ImagePath = item.imagePath;

                    specialContributions.Add(dto);
                }
                return specialContributions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialContributionDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<SpecialContributionDetailDTO> specialContributions = new List<SpecialContributionDetailDTO>();
                int counter = 0;

                var list = (from s in db.SPECIAL_CONTRIBUTIONS.Where(x => x.isDeleted == isDeleted)
                            join m in db.MEMBER on s.supervisorID equals m.memberID
                            select new
                            {
                                specialContributionID = s.specialContributionID,
                                title = s.title,
                                summary = s.summary,
                                amountEach = s.amountToContribute,
                                amountExpected = s.amountExpected,
                                supervisorID = m.memberID,
                                supervisorName = m.name,
                                supervisorSurname = m.surname,
                                imagePath = m.imagePath,
                                contributionStartDate = s.contributionStartDate,
                                contributionEndDate = s.contributionEndDate,
                            }).OrderByDescending(x => x.contributionStartDate.Year)
                            .ThenByDescending(x => x.contributionStartDate.Month)
                            .ThenByDescending(x => x.contributionStartDate.Day)
                            .ThenBy(x => x.title).ToList();

                foreach (var item in list)
                {
                    SpecialContributionDetailDTO dto = new SpecialContributionDetailDTO();
                    List<decimal> amountsContributed = new List<decimal>();

                    dto.Counter = ++counter;
                    dto.SpecialContributionID = item.specialContributionID;
                    dto.ContributionTitle = item.title;
                    dto.Summary = item.summary;
                    dto.AmountToContribute = item.amountEach;
                    dto.AmountToContributeWithCurrency = item.amountEach + " €";
                    dto.AmountExpected = item.amountExpected;
                    dto.AmountExpectedWithCurrency = item.amountExpected + " €";

                    int contributorsCount = db.SPECIAL_CONTRIBUTORS.Count(x => x.isDeleted == false && x.specialContributionID == item.specialContributionID);
                    var contributors = db.SPECIAL_CONTRIBUTORS.Where(x => x.isDeleted == false && x.specialContributionID == item.specialContributionID).ToList();
                    foreach (var contributor in contributors)
                    {
                        if (contributor.amountContributed > 0)
                        {
                            amountsContributed.Add(contributor.amountContributed);
                        }
                        else
                        {
                            amountsContributed.Add(0);
                        }
                    }
                    int totalContributors = contributorsCount;
                    decimal totalAmountContributed = amountsContributed.Sum();

                    dto.Members = totalContributors;
                    dto.AmountContributed = totalAmountContributed;
                    dto.AmountContributedWithCurrency = totalAmountContributed + " €";
                    if (totalAmountContributed < item.amountExpected)
                    {
                        dto.Status = "Incomplete";
                    }
                    else if (totalAmountContributed == item.amountExpected)
                    {
                        dto.Status = "Completed";
                    }
                    else
                    {
                        dto.Status = "Excess";
                    }

                    dto.SupervisorID = item.supervisorID;
                    dto.SupervisorName = item.supervisorName;
                    dto.SupervisorSurname = item.supervisorSurname;
                    dto.ContributionStartDate = item.contributionStartDate;
                    dto.StartDate = item.contributionStartDate.Day + "." + item.contributionStartDate.Month + "." + item.contributionStartDate.Year;
                    dto.ContributionEndDate = item.contributionEndDate;
                    dto.EndDate = item.contributionEndDate.Day + "." + item.contributionEndDate.Month + "." + item.contributionEndDate.Year;
                    dto.ImagePath = item.imagePath;

                    specialContributions.Add(dto);
                }
                return specialContributions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(SPECIAL_CONTRIBUTIONS entity)
        {
            try
            {
                SPECIAL_CONTRIBUTIONS specialContribution = db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == entity.specialContributionID);
                specialContribution.title = entity.title;
                specialContribution.summary = entity.summary;
                specialContribution.amountToContribute = entity.amountToContribute;
                specialContribution.amountExpected = entity.amountExpected;
                specialContribution.contributionStartDate = entity.contributionStartDate;
                specialContribution.contributionEndDate = entity.contributionEndDate;
                specialContribution.supervisorID = entity.supervisorID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
