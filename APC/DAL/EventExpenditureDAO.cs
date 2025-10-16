using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL
{
    public class EventExpenditureDAO : APCContexts, IDAO<EventExpenditureDetailDTO, EVENT_EXPENDITURE>
    {
        public bool Delete(EVENT_EXPENDITURE entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(EVENT_EXPENDITURE entity)
        {
            try
            {
                db.EVENT_EXPENDITURE.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventExpenditureDetailDTO> Select()
        {
            throw new NotImplementedException();
        }

        public List<EventExpenditureDetailDTO> Select(int eventID)
        {
            try
            {
                List<EventExpenditureDetailDTO> expenditures = new List<EventExpenditureDetailDTO>();
                var list = (from e in db.EVENT_EXPENDITURE.Where(x => x.isDeleted == false && x.eventID == eventID)
                            join m in db.MONTH on e.monthID equals m.monthID
                            select new
                            {
                                eventExpenditureID = e.eventExpenditureID,
                                amountSpent = e.amountSpent,
                                summary = e.summary,
                                day = e.day,
                                monthID = e.monthID,
                                monthName = m.monthName,
                                year = e.year,
                                expenditureDate = e.expenditureDate,
                            }).OrderByDescending(x => x.year).OrderByDescending(x => x.monthID).OrderByDescending(x => x.day).ToList();
                foreach (var item in list)
                {
                    EventExpenditureDetailDTO dto = new EventExpenditureDetailDTO();
                    dto.EventExpenditureID = item.eventExpenditureID;
                    dto.Summary = item.summary;
                    dto.AmountSpent = item.amountSpent;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year;
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

        public bool Update(EVENT_EXPENDITURE entity)
        {
            try
            {
                EVENT_EXPENDITURE expenditure = db.EVENT_EXPENDITURE.First(x => x.eventExpenditureID == entity.eventExpenditureID);
                expenditure.summary = entity.summary;
                expenditure.eventID = entity.eventID;
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
