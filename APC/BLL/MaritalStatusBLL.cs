using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;

namespace APC.BLL
{
    public class MaritalStatusBLL : IBLL<MaritalStatusDTO, MaritalStatusDetailDTO>
    {
        MaritalStatusDAO dao = new MaritalStatusDAO();
        public bool Delete(MaritalStatusDetailDTO entity)
        {
            MARITAL_STATUS maritalStatus = new MARITAL_STATUS();
            maritalStatus.maritalStatusID = entity.MaritalStatusID;
            return dao.Delete(maritalStatus);
        }

        public bool GetBack(MaritalStatusDetailDTO entity)
        {
            return dao.GetBack(entity.MaritalStatusID);
        }

        public bool Insert(MaritalStatusDetailDTO entity)
        {
            MARITAL_STATUS maritalStatus = new MARITAL_STATUS();
            maritalStatus.maritalStatus = entity.MaritalStatus;
            return dao.Insert(maritalStatus);
        }

        public MaritalStatusDTO Select()
        {
            MaritalStatusDTO dto = new MaritalStatusDTO();
            dto.MaritalStatuses = dao.Select();
            return dto;
        }
        
        public MaritalStatusDTO Select(bool isDeleted)
        {
            MaritalStatusDTO dto = new MaritalStatusDTO();
            dto.MaritalStatuses = dao.Select(isDeleted);
            return dto;
        }

        public bool Update(MaritalStatusDetailDTO entity)
        {
            MARITAL_STATUS maritalStatus = new MARITAL_STATUS();
            maritalStatus.maritalStatusID = entity.MaritalStatusID;
            maritalStatus.maritalStatus = entity.MaritalStatus;
            return dao.Update(maritalStatus);
        }
    }
}
