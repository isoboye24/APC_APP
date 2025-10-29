using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class SpecialContributorsDAO : APCContexts, IDAO<SpecialContributorDetailDTO, SPECIAL_CONTRIBUTORS>
    {
        public bool Delete(SPECIAL_CONTRIBUTORS entity)
        {
            try
            {
                SPECIAL_CONTRIBUTORS contributor = db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == entity.specialContributorID);
                contributor.isDeleted = true;
                contributor.deletedDate = DateTime.Today;
                db.SaveChanges();

                return true;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                SPECIAL_CONTRIBUTORS contributor = db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == ID);
                contributor.isDeleted = false;
                contributor.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(SPECIAL_CONTRIBUTORS entity)
        {
            try
            {
                db.SPECIAL_CONTRIBUTORS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialContributorDetailDTO> Select()
        {
            throw new NotImplementedException();
        }

        public List<SpecialContributorDetailDTO> Select(int contributionID)
        {
            try
            {
                List<SpecialContributorDetailDTO> contributors = new List<SpecialContributorDetailDTO>();
                int counter = 0;

                var list = (from s in db.SPECIAL_CONTRIBUTORS.Where(x => x.isDeleted == false && x.specialContributionID == contributionID)
                            join m in db.MEMBER on s.memberID equals m.memberID
                            join sn in db.SPECIAL_CONTRIBUTIONS.Where(x => x.isDeleted == false) on s.specialContributionID equals sn.specialContributionID
                            select new
                            {
                                contributorID = s.specialContributorID,
                                memberID = s.memberID,
                                name = m.name,
                                surname = m.surname,
                                imagePath = m.imagePath,
                                amountExpected = sn.amountToContribute,
                                amountContributed = s.amountContributed,
                                dateContributed = s.contributedDate,
                                summary = s.summary,
                                contributionID = s.specialContributionID,
                            }).OrderByDescending(x => x.dateContributed).OrderBy(x => x.name).ToList();

                foreach (var item in list)
                {
                    SpecialContributorDetailDTO dto = new SpecialContributorDetailDTO();
                    dto.ContributorID = item.contributorID;
                    dto.MemberID = item.memberID;
                    dto.Counter = ++counter;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.ImagePath = item.imagePath;
                    dto.AmountExpected = item.amountExpected;
                    dto.AmountExpectedWithCurrency = item.amountExpected + " €";
                    dto.AmountContributed = item.amountContributed;
                    dto.AmountContributedWithCurrency = item.amountContributed + " €";
                    dto.Balance = Math.Abs(item.amountExpected - item.amountContributed) + " €";

                    if (item.amountContributed <= 0)
                    {
                        dto.AmountContributedStatus = "Not Started";
                    }
                    else if (item.amountContributed > 0 && item.amountContributed < item.amountExpected )
                    {
                        dto.AmountContributedStatus = "Not completed";
                    }
                    else if (item.amountContributed == item.amountExpected)
                    {
                        dto.AmountContributedStatus = "Completed";
                    }
                    else
                    {
                        dto.AmountContributedStatus = "Paid Extra";
                    }
                    dto.ContributedDate = item.dateContributed;
                    dto.Date = item.dateContributed.Day + "." +item.dateContributed.Month + "." +item.dateContributed.Year;
                    dto.ContributionID = item.contributionID;
                    dto.Summary = item.summary;
                    
                    contributors.Add(dto);
                }

                return contributors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialContributorDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<SpecialContributorDetailDTO> contributors = new List<SpecialContributorDetailDTO>();
                int counter = 0;

                var list = (from s in db.SPECIAL_CONTRIBUTORS.Where(x => x.isDeleted == isDeleted)
                            join m in db.MEMBER on s.memberID equals m.memberID
                            join sn in db.SPECIAL_CONTRIBUTIONS.Where(x => x.isDeleted == false) on s.specialContributionID equals sn.specialContributionID
                            select new
                            {
                                contributorID = s.specialContributorID,
                                memberID = s.memberID,
                                name = m.name,
                                surname = m.surname,
                                imagePath = m.imagePath,
                                amountExpected = sn.amountToContribute,
                                amountContributed = s.amountContributed,
                                dateContributed = s.contributedDate,
                                summary = s.summary,
                                contributionID = s.specialContributionID,
                            }).OrderByDescending(x => x.dateContributed).OrderBy(x => x.name).ToList();

                foreach (var item in list)
                {
                    SpecialContributorDetailDTO dto = new SpecialContributorDetailDTO();
                    dto.ContributorID = item.contributorID;
                    dto.MemberID = item.memberID;
                    dto.Counter = ++counter;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.ImagePath = item.imagePath;
                    dto.AmountExpected = item.amountExpected;
                    dto.AmountExpectedWithCurrency = item.amountExpected + " €";
                    dto.AmountContributed = item.amountContributed;
                    dto.AmountContributedWithCurrency = item.amountContributed + " €";
                    dto.Balance = Math.Abs(item.amountExpected - item.amountContributed) + " €";

                    if (item.amountContributed <= 0)
                    {
                        dto.AmountContributedStatus = "Not Started";
                    }
                    else if (item.amountContributed > 0 && item.amountContributed < item.amountExpected)
                    {
                        dto.AmountContributedStatus = "Not completed";
                    }
                    else if (item.amountContributed == item.amountExpected)
                    {
                        dto.AmountContributedStatus = "Completed";
                    }
                    else
                    {
                        dto.AmountContributedStatus = "Paid Extra";
                    }
                    dto.ContributedDate = item.dateContributed;
                    dto.Date = item.dateContributed.Day + "." + item.dateContributed.Month + "." + item.dateContributed.Year;
                    dto.ContributionID = item.contributionID;
                    dto.Summary = item.summary;

                    contributors.Add(dto);
                }

                return contributors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SPECIAL_CONTRIBUTORS entity)
        {
            try
            {
                SPECIAL_CONTRIBUTORS contributor = db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == entity.specialContributorID);
                contributor.summary = entity.summary;
                contributor.contributedDate = entity.contributedDate;
                contributor.memberID = entity.memberID;
                contributor.specialContributionID = entity.specialContributionID;
                contributor.amountContributed = entity.amountContributed;
                
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
