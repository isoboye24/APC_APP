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
    public class EventReceiptsBLL : IBLL<EventReceiptsDTO, EventReceiptsDetailDTO>
    {
        EventReceiptsDAO dao = new EventReceiptsDAO();
        MonthDAO monthDAO = new MonthDAO();
        public bool Delete(EventReceiptsDetailDTO entity)
        {
            EVENT_RECEIPTS receipt = new EVENT_RECEIPTS();
            receipt.eventReceiptID = entity.EventReceiptID;
            return dao.Delete(receipt);
        }

        public bool GetBack(EventReceiptsDetailDTO entity)
        {
            return dao.GetBack(entity.EventReceiptID);
        }

        public bool Insert(EventReceiptsDetailDTO entity)
        {
            EVENT_RECEIPTS receipt = new EVENT_RECEIPTS();
            receipt.eventID = entity.EventID;
            receipt.summary = entity.Summary;
            receipt.caption = entity.ImageCaption;
            receipt.amountSpent = entity.AmountSpent;
            receipt.receiptDate = entity.ReceiptDate;
            receipt.day = entity.Day;
            receipt.monthID = entity.MonthID;
            receipt.year = entity.Year;
            receipt.imagePath = entity.ImagePath;

            return dao.Insert(receipt);
        }

        public EventReceiptsDTO Select()
        {
            throw new NotImplementedException();
        }
        
        public EventReceiptsDTO Select(int eventID)
        {
            EventReceiptsDTO dto = new EventReceiptsDTO();
            dto.EventReceipts = dao.Select(eventID);
            dto.Months = monthDAO.Select();

            return dto;
        }

        public decimal SelectTotalAmountOnEventReceipt(int eventID)
        {
            return dao.SelectTotalAmountOnEventReceipt(eventID);
        }

        public bool Update(EventReceiptsDetailDTO entity)
        {
            EVENT_RECEIPTS receipt = new EVENT_RECEIPTS();
            receipt.eventReceiptID = entity.EventReceiptID;
            receipt.eventID = entity.EventID;
            receipt.summary = entity.Summary;
            receipt.caption = entity.ImageCaption;
            receipt.amountSpent = entity.AmountSpent;
            receipt.receiptDate = entity.ReceiptDate;
            receipt.day = entity.Day;
            receipt.monthID = entity.MonthID;
            receipt.year = entity.Year;
            receipt.imagePath = entity.ImagePath;

            return dao.Update(receipt);
        }
    }
}
