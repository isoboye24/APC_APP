using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APC.DAL.DTO;

namespace APC.DAL.DAO
{
    public class MaritalStatusDAO : APCContexts, IDAO<MaritalStatusDetailDTO, MARITAL_STATUS>
    {
        public bool Delete(MARITAL_STATUS entity)
        {
            try
            {
                MARITAL_STATUS maritalStatus = db.MARITAL_STATUS.First(x=>x.maritalStatusID==entity.maritalStatusID);
                maritalStatus.isDeleted = true;
                maritalStatus.deletedDate = DateTime.Today;
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
                MARITAL_STATUS marStatus = db.MARITAL_STATUS.First(x=>x.maritalStatusID==ID);
                marStatus.isDeleted = false;
                marStatus.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(MARITAL_STATUS entity)
        {
            try
            {
                db.MARITAL_STATUS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MaritalStatusDetailDTO> Select()
        {
            try
            {
                List<MaritalStatusDetailDTO> maritalStatuses = new List<MaritalStatusDetailDTO>();
                var list = db.MARITAL_STATUS.Where(x => x.isDeleted == false).OrderBy(x => x.maritalStatus).ToList();
                foreach (var item in list)
                {
                    MaritalStatusDetailDTO dto = new MaritalStatusDetailDTO();
                    dto.MaritalStatusID = item.maritalStatusID;
                    dto.MaritalStatus = item.maritalStatus;
                    maritalStatuses.Add(dto);
                }
                return maritalStatuses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MaritalStatusDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<MaritalStatusDetailDTO> maritalStatuses = new List<MaritalStatusDetailDTO>();
                var list = db.MARITAL_STATUS.Where(x => x.isDeleted == isDeleted).OrderBy(x => x.maritalStatus).ToList();
                foreach (var item in list)
                {
                    MaritalStatusDetailDTO dto = new MaritalStatusDetailDTO();
                    dto.MaritalStatusID = item.maritalStatusID;
                    dto.MaritalStatus = item.maritalStatus;
                    maritalStatuses.Add(dto);
                }
                return maritalStatuses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(MARITAL_STATUS entity)
        {
            try
            {
                MARITAL_STATUS maritalStatus = db.MARITAL_STATUS.First(x => x.maritalStatusID == entity.maritalStatusID);
                maritalStatus.maritalStatus = entity.maritalStatus;
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
