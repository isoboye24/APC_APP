using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class EventReceiptsDAO : APCContexts, IDAO<EventReceiptsDetailDTO, EVENT_RECEIPTS>
    {
        public bool Delete(EVENT_RECEIPTS entity)
        {
            try
            {
                EVENT_RECEIPTS receipt = db.EVENT_RECEIPTS.First(x => x.eventReceiptID == entity.eventReceiptID);
                receipt.isDeleted = true;
                receipt.deletedDate = DateTime.Today;
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
                EVENT_RECEIPTS receipt = db.EVENT_RECEIPTS.First(x => x.eventReceiptID == ID);
                receipt.isDeleted = false;
                receipt.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EVENT_RECEIPTS entity)
        {
            try
            {
                db.EVENT_RECEIPTS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventReceiptsDetailDTO> Select()
        {
            throw new NotImplementedException();
        }
        
        public List<EventReceiptsDetailDTO> Select(int eventID)
        {
            try
            {
                List<EventReceiptsDetailDTO> eventreceipts = new List<EventReceiptsDetailDTO>();
                int counter = 0;
                var list = (from er in db.EVENT_RECEIPTS.Where(x => x.isDeleted == false && x.eventID == eventID)
                            join m in db.MONTH on er.monthID equals m.monthID
                            select new
                            {
                                eventReceiptID = er.eventReceiptID,
                                eventID = er.eventID,
                                summary = er.summary,
                                imagePath = er.imagePath,
                                imageCaption = er.caption,
                                amountSpent = er.amountSpent,
                                day = er.day,
                                monthID = er.monthID,
                                monthName = m.monthName,
                                year = er.year,
                                receiptDate = er.receiptDate,
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.imageCaption).ToList();
                foreach (var item in list)
                {
                    EventReceiptsDetailDTO dto = new EventReceiptsDetailDTO();
                    dto.EventReceiptID = item.eventReceiptID;
                    dto.EventID = item.eventID;
                    dto.Summary = item.summary;
                    dto.ImagePath = item.imagePath;
                    dto.ImageCaption = item.imageCaption;
                    dto.AmountSpent = item.amountSpent;
                    dto.AmountSpentWithCurrency = item.amountSpent + " €";
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year;
                    dto.ReceiptDate = item.receiptDate;
                    dto.Counter = ++counter;

                    eventreceipts.Add(dto);
                }
                return eventreceipts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventReceiptsDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<EventReceiptsDetailDTO> eventreceipts = new List<EventReceiptsDetailDTO>();
                int counter = 0;
                var list = (from er in db.EVENT_RECEIPTS.Where(x => x.isDeleted == isDeleted)
                            join m in db.MONTH on er.monthID equals m.monthID
                            select new
                            {
                                eventReceiptID = er.eventReceiptID,
                                eventID = er.eventID,
                                summary = er.summary,
                                imagePath = er.imagePath,
                                imageCaption = er.caption,
                                amountSpent = er.amountSpent,
                                day = er.day,
                                monthID = er.monthID,
                                monthName = m.monthName,
                                year = er.year,
                                receiptDate = er.receiptDate,
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.imageCaption).ToList();
                foreach (var item in list)
                {
                    EventReceiptsDetailDTO dto = new EventReceiptsDetailDTO();
                    dto.EventReceiptID = item.eventReceiptID;
                    dto.EventID = item.eventID;
                    dto.Summary = item.summary;
                    dto.ImagePath = item.imagePath;
                    dto.ImageCaption = item.imageCaption;
                    dto.AmountSpent = item.amountSpent;
                    dto.AmountSpentWithCurrency = item.amountSpent + " €";
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year;
                    dto.ReceiptDate = item.receiptDate;
                    dto.Counter = ++counter;

                    eventreceipts.Add(dto);
                }
                return eventreceipts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal SelectTotalAmountOnEventReceipt(int eventID)
        {
            try
            {
                List<decimal> totalEventReceiptAmount = new List<decimal>();
                var list = db.EVENT_RECEIPTS.Where(x => x.isDeleted == false && x.eventID == eventID);
                foreach (var item in list)
                {
                    totalEventReceiptAmount.Add(item.amountSpent);
                }
                decimal total = totalEventReceiptAmount.Sum();
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

        public bool Update(EVENT_RECEIPTS entity)
        {
            try
            {
                EVENT_RECEIPTS eventReceipt = db.EVENT_RECEIPTS.First(x => x.eventReceiptID == entity.eventReceiptID);
                eventReceipt.eventID = entity.eventID;
                eventReceipt.summary = entity.summary;
                eventReceipt.caption = entity.caption;
                eventReceipt.imagePath = entity.imagePath;
                eventReceipt.amountSpent = entity.amountSpent;
                eventReceipt.day = entity.day;
                eventReceipt.monthID = entity.monthID;
                eventReceipt.year = entity.year;
                eventReceipt.receiptDate = entity.receiptDate;
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
