using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class PaymentStatusDAO : IDAO<PaymentStatusDetailDTO, PAYMENT_STATUS>
    {
        public bool Delete(PAYMENT_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PAYMENT_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public List<PaymentStatusDetailDTO> Select()
        {
            throw new NotImplementedException();
        }

        public bool Update(PAYMENT_STATUS entity)
        {
            throw new NotImplementedException();
        }
    }
}
