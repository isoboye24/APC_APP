using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class PaymentStatusDAO : APCContexts, IDAO<PaymentStatusDetailDTO, PAYMENT_STATUS>
    {
        public bool Delete(PAYMENT_STATUS entity)
        {
            try
            {
                PAYMENT_STATUS status = db.PAYMENT_STATUS.First(x => x.paymentStatusID == entity.paymentStatusID);
                status.isDeleted = true;
                status.deletedDate = DateTime.Today;
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
            throw new NotImplementedException();
        }

        public bool Insert(PAYMENT_STATUS entity)
        {
            try
            {
                db.PAYMENT_STATUS.Add(entity);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentStatusDetailDTO> Select()
        {
            try
            {
                List<PaymentStatusDetailDTO> statuses = new List<PaymentStatusDetailDTO>();
                var list = db.PAYMENT_STATUS.Where(x => x.isDeleted == false).OrderBy(x => x.paymentStatusName).ToList();
                foreach (var item in list)
                {
                    PaymentStatusDetailDTO dto = new PaymentStatusDetailDTO();
                    dto.PaymentStatusID = item.paymentStatusID;
                    dto.PaymentStatusName = item.paymentStatusName;
                    statuses.Add(dto);
                }
                return statuses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(PAYMENT_STATUS entity)
        {
            try
            {
                PAYMENT_STATUS status = db.PAYMENT_STATUS.First(x => x.paymentStatusID == entity.paymentStatusID);
                status.paymentStatusName = entity.paymentStatusName;
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
