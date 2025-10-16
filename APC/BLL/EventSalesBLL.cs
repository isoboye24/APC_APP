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
    public class EventSalesBLL : IBLL<EventSalesDTO, EventSalesDetailDTO>
    {
        EventSalesDAO dao = new EventSalesDAO();
        MonthDAO monthDAO = new MonthDAO();

        public bool Delete(EventSalesDetailDTO entity)
        {
            EVENT_SALES sales = new EVENT_SALES();
            sales.eventSalesID = entity.EventSalesID;
            return dao.Delete(sales);
        }

        public bool GetBack(EventSalesDetailDTO entity)
        {
            return dao.GetBack(entity.EventSalesID);
        }

        public bool Insert(EventSalesDetailDTO entity)
        {
            EVENT_SALES sales = new EVENT_SALES();
            sales.eventID = entity.EventID;
            sales.amountSold = entity.AmountSold;
            sales.salesDate = entity.SalesDate;
            sales.summary = entity.Summary;
            sales.day = entity.Day;
            sales.monthID = entity.MonthID;
            sales.year = entity.Year;

            return dao.Insert(sales);
        }

        public EventSalesDTO Select(int eventID)
        {
            EventSalesDTO dto = new EventSalesDTO();
            dto.EventSales = dao.Select(eventID);
            dto.Months = monthDAO.Select();
            return dto;
        }

        public EventSalesDTO Select()
        {
            throw new NotImplementedException();
        }

        public decimal SelectTotalAmountEventSold(int eventID)
        {
            return dao.SelectTotalAmountEventSold(eventID);
        }

        public bool Update(EventSalesDetailDTO entity)
        {
            EVENT_SALES sales = new EVENT_SALES();
            sales.eventSalesID = entity.EventSalesID;
            sales.eventID = entity.EventID;
            sales.amountSold = entity.AmountSold;
            sales.salesDate = entity.SalesDate;
            sales.summary = entity.Summary;
            sales.day = entity.Day;
            sales.monthID = entity.MonthID;
            sales.year = entity.Year;

            return dao.Update(sales);
        }
    }
}
