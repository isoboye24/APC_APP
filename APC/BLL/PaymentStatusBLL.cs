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
    public class PaymentStatusBLL : IBLL<PaymentStatusDTO, PaymentStatusDetailDTO>
    {
        PaymentStatusDAO dao = new PaymentStatusDAO();
        public bool Delete(PaymentStatusDetailDTO entity)
        {
            PAYMENT_STATUS status = new PAYMENT_STATUS();
            status.paymentStatusID = entity.PaymentStatusID;
            return dao.Delete(status);
        }

        public bool GetBack(PaymentStatusDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PaymentStatusDetailDTO entity)
        {
            PAYMENT_STATUS status = new PAYMENT_STATUS();
            status.paymentStatusName = entity.PaymentStatusName;
            return dao.Insert(status);
        }

        public PaymentStatusDTO Select()
        {
            PaymentStatusDTO dto = new PaymentStatusDTO();
            dto.PaymentStatuses = dao.Select();
            return dto;
        }

        public bool Update(PaymentStatusDetailDTO entity)
        {
            PAYMENT_STATUS status = new PAYMENT_STATUS();
            status.paymentStatusID = entity.PaymentStatusID;
            status.paymentStatusName = entity.PaymentStatusName;
            return dao.Update(status);
        }
    }
}
