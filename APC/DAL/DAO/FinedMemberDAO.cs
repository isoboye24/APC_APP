using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace APC.DAL.DAO
{
    public class FinedMemberDAO : APCContexts, IDAO<FinedMemberDetailDTO, FINED_MEMBER>
    {
        public bool Delete(FINED_MEMBER entity)
        {
            try
            {
                FINED_MEMBER finedMember = db.FINED_MEMBER.First(x => x.finedMemberID == entity.finedMemberID);
                finedMember.isdeleted = true;
                finedMember.deletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelectAllFinesCount(int ID)
        {
            try
            {
                int finesCount = db.FINED_MEMBER.Count(x=>x.memberID == ID && x.isdeleted == false);
                return finesCount;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        
        public decimal SelectTotalFinedCount()
        {
            try
            {
                int finesCount = db.FINED_MEMBER.Count(x=>x.isdeleted == false);
                return finesCount;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public decimal SelectTotalFinedExpected()
        {
            try
            {
                List<decimal> totalFinesExpected = new List<decimal>();
                var list = db.FINED_MEMBER.Where(x=>x.isdeleted == false);
                foreach (var item in list)
                {
                    CONSTITUTION fine = db.CONSTITUTIONs.First(x => x.isDeleted == false && x.constitutionID == item.constitutionID);
                    totalFinesExpected.Add(fine.fine);
                }
                decimal totalAmount = totalFinesExpected.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        
        public decimal TotalPaidFines()
        {
            try
            {
                List<decimal> totalPaidFines = new List<decimal>();
                var allFines = db.FINED_MEMBER.Where(x => x.isdeleted == false).ToList();
                foreach (var fine in allFines)
                {
                    totalPaidFines.Add((decimal)fine.amountPaid);
                }
                decimal totalFinesPaid = totalPaidFines.Sum();
                return totalFinesPaid;
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
                FINED_MEMBER finedMember = db.FINED_MEMBER.First(x => x.finedMemberID == ID);
                finedMember.isdeleted = false;
                finedMember.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(FINED_MEMBER entity)
        {
            try
            {
                db.FINED_MEMBER.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FinedMemberDetailDTO> Select()
        {
            try
            {
                List<FinedMemberDetailDTO> finedMembers = new List<FinedMemberDetailDTO>();
                var list = (from fm in db.FINED_MEMBER.Where(x => x.isdeleted == false)
                            join m in db.MEMBERs.Where(x=>x.isDeleted == false) on fm.memberID equals m.memberID
                            join ms in db.MEMBERSHIP_STATUS.Where(x => x.membershipStatusID == 1) on m.membershipStatusID equals ms.membershipStatusID
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join p in db.POSITIONs on m.positionID equals p.positionID
                            join mn in db.MONTHs on fm.monthID equals mn.monthID
                            join c in db.CONSTITUTIONs on fm.constitutionID equals c.constitutionID
                            select new
                            {
                                finedMemberID = fm.finedMemberID,
                                amountPaid = fm.amountPaid,
                                constitutionID = fm.constitutionID,
                                expectedAmount = c.fine,
                                summary = fm.summary,
                                section = c.section,
                                constitution = c.constitution1,
                                shortDescription = c.ShortDescription,
                                memberID = m.memberID,
                                name = m.name,
                                surname = m.surname,
                                positionID = m.positionID,
                                imagePath = m.imagePath,
                                position = p.positionName,
                                genderID = m.genderID,
                                gender = g.genderName,
                                day = fm.day,
                                monthID = fm.monthID,
                                month = mn.monthName,
                                year = fm.year,
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.name).ToList();
                foreach (var item in list)
                {
                    FinedMemberDetailDTO dto = new FinedMemberDetailDTO();
                    dto.FinedMemberID = item.finedMemberID;
                    dto.AmountPaid = Convert.ToDecimal(item.amountPaid);
                    dto.AmountPaidWithCurrency = "€ " + dto.AmountPaid;
                    dto.ConstitutionID = item.constitutionID;
                    dto.ExpectedAmount = item.expectedAmount;
                    dto.ExpectedAmountWithCurrency = "€ " + dto.ExpectedAmount;
                    dto.Balance = dto.ExpectedAmount - dto.AmountPaid;
                    dto.BalanceWithCurrency = "€ " + dto.Balance;
                    dto.Summary = item.summary;
                    if (dto.Balance == 0)
                    {
                        dto.FineStatus = "Completed";
                    }
                    else if (dto.Balance > 0)
                    {
                        dto.FineStatus = "Extra";
                    }
                    else
                    {
                        dto.FineStatus = "NOT completed";
                    }
                    dto.ConstitutionSection = item.section;
                    dto.ConstitutionShortDescription = item.shortDescription;
                    dto.Constitution = item.constitution;
                    dto.MemberID = item.memberID;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.PositionID = item.positionID;
                    dto.Position = item.position;
                    dto.GenderID = item.genderID;
                    dto.Gender = item.gender;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.month;
                    dto.Year = item.year;
                    dto.ImagePath = item.imagePath;
                    finedMembers.Add(dto);
                }
                return finedMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<FinedMemberDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<FinedMemberDetailDTO> finedMembers = new List<FinedMemberDetailDTO>();
                var list = (from fm in db.FINED_MEMBER.Where(x => x.isdeleted == isDeleted)
                            join m in db.MEMBERs.Where(x=>x.isDeleted == false) on fm.memberID equals m.memberID
                            join ms in db.MEMBERSHIP_STATUS.Where(x => x.membershipStatusID == 1) on m.membershipStatusID equals ms.membershipStatusID
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join p in db.POSITIONs on m.positionID equals p.positionID
                            join mn in db.MONTHs on fm.monthID equals mn.monthID
                            join c in db.CONSTITUTIONs on fm.constitutionID equals c.constitutionID
                            select new
                            {
                                finedMemberID = fm.finedMemberID,
                                amountPaid = fm.amountPaid,
                                constitutionID = fm.constitutionID,
                                expectedAmount = c.fine,
                                summary = fm.summary,
                                section = c.section,
                                constitution = c.constitution1,
                                shortDescription = c.ShortDescription,
                                memberID = m.memberID,
                                name = m.name,
                                surname = m.surname,
                                positionID = m.positionID,
                                imagePath = m.imagePath,
                                position = p.positionName,
                                genderID = m.genderID,
                                gender = g.genderName,
                                day = fm.day,
                                monthID = fm.monthID,
                                month = mn.monthName,
                                year = fm.year,
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.name).ToList();
                foreach (var item in list)
                {
                    FinedMemberDetailDTO dto = new FinedMemberDetailDTO();
                    dto.FinedMemberID = item.finedMemberID;
                    dto.AmountPaid = Convert.ToDecimal(item.amountPaid);
                    dto.AmountPaidWithCurrency = "€ " + dto.AmountPaid;
                    dto.ConstitutionID = item.constitutionID;
                    dto.ExpectedAmount = item.expectedAmount;
                    dto.ExpectedAmountWithCurrency = "€ " + dto.ExpectedAmount;
                    dto.Balance = dto.ExpectedAmount - dto.AmountPaid;
                    dto.BalanceWithCurrency = "€ " + dto.Balance;
                    dto.Summary = item.summary;
                    if (dto.Balance == 0)
                    {
                        dto.FineStatus = "Completed";
                    }
                    else if (dto.Balance > 0)
                    {
                        dto.FineStatus = "Extra";
                    }
                    else
                    {
                        dto.FineStatus = "NOT completed";
                    }
                    dto.ConstitutionSection = item.section;
                    dto.Constitution = item.constitution;
                    dto.MemberID = item.memberID;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.PositionID = item.positionID;
                    dto.Position = item.position;
                    dto.GenderID = item.genderID;
                    dto.Gender = item.gender;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.month;
                    dto.Year = item.year;
                    dto.ImagePath = item.imagePath;
                    finedMembers.Add(dto);
                }
                return finedMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FinedMemberDetailDTO> SelectFinedMember(int ID)
        {
            try
            {
                List<FinedMemberDetailDTO> finedMembers = new List<FinedMemberDetailDTO>();
                var list = (from fm in db.FINED_MEMBER.Where(x => x.isdeleted == false && x.memberID == ID)
                            join m in db.MEMBERs.Where(x=>x.isDeleted == false) on fm.memberID equals m.memberID
                            join ms in db.MEMBERSHIP_STATUS on m.membershipStatusID equals ms.membershipStatusID
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join p in db.POSITIONs on m.positionID equals p.positionID
                            join mn in db.MONTHs on fm.monthID equals mn.monthID
                            join c in db.CONSTITUTIONs on fm.constitutionID equals c.constitutionID
                            select new
                            {
                                finedMemberID = fm.finedMemberID,
                                amountPaid = fm.amountPaid,
                                constitutionID = fm.constitutionID,
                                expectedAmount = c.fine,
                                summary = fm.summary,
                                section = c.section,
                                constitution = c.constitution1,
                                memberID = m.memberID,
                                name = m.name,
                                surname = m.surname,
                                positionID = m.positionID,
                                imagePath = m.imagePath,
                                position = p.positionName,
                                genderID = m.genderID,
                                gender = g.genderName,
                                day = fm.day,
                                monthID = fm.monthID,
                                month = mn.monthName,
                                year = fm.year,
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.name).ToList();
                foreach (var item in list)
                {
                    FinedMemberDetailDTO dto = new FinedMemberDetailDTO();
                    dto.FinedMemberID = item.finedMemberID;
                    dto.AmountPaid = Convert.ToDecimal(item.amountPaid);
                    dto.AmountPaidWithCurrency = "€ " + dto.AmountPaid;
                    dto.ConstitutionID = item.constitutionID;
                    dto.ExpectedAmount = item.expectedAmount;
                    dto.ExpectedAmountWithCurrency = "€ " + dto.ExpectedAmount;
                    dto.Balance = dto.ExpectedAmount - dto.AmountPaid;
                    dto.BalanceWithCurrency = "€ " + dto.Balance;
                    dto.Summary = item.summary;
                    if (dto.Balance < 1)
                    {
                        dto.FineStatus = "Completed";
                    }
                    else
                    {
                        dto.FineStatus = "NOT completed";
                    }
                    dto.ConstitutionSection = item.section;
                    dto.Constitution = item.constitution;
                    dto.MemberID = item.memberID;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.PositionID = item.positionID;
                    dto.Position = item.position;
                    dto.GenderID = item.genderID;
                    dto.Gender = item.gender;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.month;
                    dto.Year = item.year;
                    dto.ImagePath = item.imagePath;
                    finedMembers.Add(dto);
                }
                return finedMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetFinedAmountPaid(int ID)
        {
            try
            {
                List<decimal> paidAmounts = new List<decimal>();
                decimal totalPaidAmount = 0;
                var list = db.FINED_MEMBER.Where(x => x.isdeleted == false && x.memberID == ID).ToList();
                foreach (var item in list)
                {
                    paidAmounts.Add((decimal)item.amountPaid);
                }
                totalPaidAmount = paidAmounts.Sum();
                return totalPaidAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal GetFinedAmountExpected(int ID)
        {
            try
            {
                List<decimal> expectedFines = new List<decimal>();
                decimal totalExpectedAmount = 0;
                var list = (from fm in db.FINED_MEMBER.Where(x => x.isdeleted == false && x.memberID == ID)
                            join c in db.CONSTITUTIONs.Where(x=>x.isDeleted == false) on fm.constitutionID equals c.constitutionID
                            select new { 
                                finedMemberID = fm.finedMemberID,
                                memberID = fm.memberID,
                                fine = c.fine,
                            }).ToList();
                foreach (var item in list)
                {
                    expectedFines.Add((decimal)item.fine);
                }
                totalExpectedAmount = expectedFines.Sum();
                return totalExpectedAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(FINED_MEMBER entity)
        {
            try
            {
                FINED_MEMBER finedMember = new FINED_MEMBER();
                finedMember = db.FINED_MEMBER.First(x => x.finedMemberID == entity.finedMemberID);
                finedMember.amountPaid = entity.amountPaid;
                finedMember.constitutionID = entity.constitutionID;
                finedMember.summary = entity.summary;
                finedMember.memberID = entity.memberID;
                finedMember.day = entity.day;
                finedMember.monthID = entity.monthID;
                finedMember.year = entity.year;
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
