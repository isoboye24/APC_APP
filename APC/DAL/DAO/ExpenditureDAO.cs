using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class ExpenditureDAO : APCContexts, IDAO<ExpenditureDetailDTO, EXPENDITURE>
    {
        public bool Delete(EXPENDITURE entity)
        {
            try
            {
                EXPENDITURE expenditure = db.EXPENDITURE.First(x => x.expenditureID == entity.expenditureID);
                expenditure.isDeleted = true;
                expenditure.deletedDate = DateTime.Today;
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
                EXPENDITURE expenditure = db.EXPENDITURE.First(x=>x.expenditureID==ID);
                expenditure.isDeleted = false;
                expenditure.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal SelectTotalExpendituresYearly(int year)
        {
            try
            {
                List<decimal> totalExpendituresYearly = new List<decimal>();
                var list = db.EXPENDITURE.Where(x=>x.year==year && x.isDeleted == false).ToList();
                foreach (var item in list)
                {
                    totalExpendituresYearly.Add(item.amountSpent);
                }
                decimal totalAmount = totalExpendituresYearly.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal SelectTotalExpenditures()
        {
            try
            {
                List<decimal> totalExpenditures = new List<decimal>();
                var list = db.EXPENDITURE.Where(x=>x.isDeleted==false).ToList();
                foreach (var item in list)
                {
                    totalExpenditures.Add(item.amountSpent);
                }
                decimal totalAmount = totalExpenditures.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EXPENDITURE entity)
        {
            try
            {
                db.EXPENDITURE.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpenditureDetailDTO> Select()
        {
            try
            {
                List<ExpenditureDetailDTO> expenditures = new List<ExpenditureDetailDTO>();
                var list = (from e in db.EXPENDITURE.Where(x => x.isDeleted == false)
                            join m in db.MONTH on e.monthID equals m.monthID
                            select new
                            {
                                expenditureID = e.expenditureID,
                                amountSpent = e.amountSpent,
                                summary = e.summary,
                                day = e.day,
                                monthID = e.monthID,
                                monthName = m.monthName,
                                year = e.year,
                                expenditureDate = e.expenditureDate,
                            }).OrderByDescending(x => x.year).OrderByDescending(x =>x.monthID).ToList();
                foreach (var item in list)
                {
                    ExpenditureDetailDTO dto = new ExpenditureDetailDTO();
                    dto.ExpenditureID = item.expenditureID;
                    dto.Summary = item.summary;
                    dto.AmountSpent = item.amountSpent;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.Month = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.ExpenditureDate = item.expenditureDate;
                    expenditures.Add(dto);
                }
                return expenditures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ExpenditureDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<ExpenditureDetailDTO> expenditures = new List<ExpenditureDetailDTO>();
                var list = (from e in db.EXPENDITURE.Where(x => x.isDeleted == isDeleted)
                            join m in db.MONTH on e.monthID equals m.monthID
                            select new
                            {
                                expenditureID = e.expenditureID,
                                amountSpent = e.amountSpent,
                                summary = e.summary,
                                day = e.day,
                                monthID = e.monthID,
                                monthName = m.monthName,
                                year = e.year,
                                expenditureDate = e.expenditureDate,
                            }).OrderByDescending(x => x.year).ToList();
                foreach (var item in list)
                {
                    ExpenditureDetailDTO dto = new ExpenditureDetailDTO();
                    dto.ExpenditureID = item.expenditureID;
                    dto.Summary = item.summary;
                    dto.AmountSpent = item.amountSpent;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.Month = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.ExpenditureDate = item.expenditureDate;
                    expenditures.Add(dto);
                }
                return expenditures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EXPENDITURE entity)
        {
            try
            {
                EXPENDITURE expenditure = db.EXPENDITURE.First(x => x.expenditureID == entity.expenditureID);
                expenditure.summary = entity.summary;
                expenditure.amountSpent = entity.amountSpent;
                expenditure.day = entity.day;
                expenditure.monthID = entity.monthID;
                expenditure.year = entity.year;
                expenditure.expenditureDate = entity.expenditureDate;
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
