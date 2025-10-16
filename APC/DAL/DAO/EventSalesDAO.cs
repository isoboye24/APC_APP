using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class EventSalesDAO : APCContexts, IDAO<EventSalesDetailDTO, EVENT_SALES>
    {
        public bool Delete(EVENT_SALES entity)
        {
            try
            {
                EVENT_SALES sales = db.EVENT_SALES.First(x => x.eventSalesID == entity.eventSalesID);
                sales.isDeleted = true;
                sales.deletedDate = DateTime.Today;
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
                EVENT_SALES sales = db.EVENT_SALES.First(x => x.eventSalesID == ID);
                sales.isDeleted = false;
                sales.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EVENT_SALES entity)
        {
            try
            {
                db.EVENT_SALES.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventSalesDetailDTO> Select()
        {
            throw new NotImplementedException();
        }

        public List<EventSalesDetailDTO> Select(int eventID)
        {
            try
            {
                List<EventSalesDetailDTO> sales = new List<EventSalesDetailDTO>();
                var list = (from e in db.EVENT_SALES.Where(x => x.isDeleted == false && x.eventID == eventID)
                            join m in db.MONTH on e.monthID equals m.monthID
                            select new
                            {
                                eventSalesID = e.eventSalesID,
                                eventID = e.eventID,
                                amountSold = e.amountSold,
                                summary = e.summary,
                                day = e.day,
                                monthID = e.monthID,
                                monthName = m.monthName,
                                year = e.year,
                                salesDate = e.salesDate,
                            }).OrderByDescending(x => x.year).OrderByDescending(x => x.monthID).OrderByDescending(x => x.day).ToList();
                foreach (var item in list)
                {
                    EventSalesDetailDTO dto = new EventSalesDetailDTO();
                    dto.EventSalesID = item.eventSalesID;
                    dto.EventID = item.eventID;
                    dto.Summary = item.summary;
                    dto.AmountSold = item.amountSold;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year;
                    dto.SalesDate = item.salesDate;
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal SelectTotalAmountEventSold(int eventID)
        {
            try
            {
                List<decimal> totalEventSales = new List<decimal>();
                var list = db.EVENT_SALES.Where(x => x.isDeleted == false && x.eventID == eventID);
                foreach (var item in list)
                {
                    totalEventSales.Add(item.amountSold);
                }
                decimal total = totalEventSales.Sum();
                if (total > 0)
                {
                    return total;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EVENT_SALES entity)
        {
            try
            {
                EVENT_SALES sales = db.EVENT_SALES.First(x => x.eventSalesID == entity.eventSalesID);
                sales.summary = entity.summary;
                sales.eventID = entity.eventID;
                sales.amountSold = entity.amountSold;
                sales.day = entity.day;
                sales.monthID = entity.monthID;
                sales.year = entity.year;
                sales.salesDate = entity.salesDate;
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
