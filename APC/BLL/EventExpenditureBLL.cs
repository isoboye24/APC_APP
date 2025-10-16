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
    public class EventExpenditureBLL : IBLL<EventExpenditureDTO, EventExpenditureDetailDTO>
    {
        EventExpenditureDAO dao = new EventExpenditureDAO();
        MonthDAO monthDAO = new MonthDAO();
        public bool Delete(EventExpenditureDetailDTO entity)
        {
            EVENT_EXPENDITURE expenditure = new EVENT_EXPENDITURE();
            expenditure.eventExpenditureID = entity.EventExpenditureID;
            return dao.Delete(expenditure);
        }

        public bool GetBack(EventExpenditureDetailDTO entity)
        {
            return dao.GetBack(entity.EventExpenditureID);
        }

        public bool Insert(EventExpenditureDetailDTO entity)
        {
            EVENT_EXPENDITURE expenditure = new EVENT_EXPENDITURE();
            expenditure.eventID = entity.EventID;
            expenditure.amountSpent = entity.AmountSpent;
            expenditure.expenditureDate = entity.ExpenditureDate;
            expenditure.summary = entity.Summary;
            expenditure.day = entity.Day;
            expenditure.monthID = entity.MonthID;
            expenditure.year = entity.Year;

            return dao.Insert(expenditure);
        }

        public EventExpenditureDTO Select()
        {
            throw new NotImplementedException();
        }

        public EventExpenditureDTO Select(int eventID)
        {
            EventExpenditureDTO dto = new EventExpenditureDTO();
            dto.EventExpenditures = dao.Select(eventID);
            dto.Months = monthDAO.Select();
            return dto;
        }
        
        public decimal SelectTotalAmountEventExp(int eventID)
        {
            return dao.SelectTotalAmountEventExp(eventID);           
        }

        public bool Update(EventExpenditureDetailDTO entity)
        {
            EVENT_EXPENDITURE expenditure = new EVENT_EXPENDITURE();
            expenditure.eventExpenditureID = entity.EventExpenditureID;
            expenditure.eventID = entity.EventID;
            expenditure.summary = entity.Summary;
            expenditure.amountSpent = entity.AmountSpent;
            expenditure.day = entity.Day;
            expenditure.monthID = entity.MonthID;
            expenditure.year = Convert.ToInt32(entity.Year);
            expenditure.expenditureDate = entity.ExpenditureDate;
            return dao.Update(expenditure);
        }
    }
}
