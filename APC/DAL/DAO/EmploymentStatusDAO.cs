using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class EmploymentStatusDAO : APCContexts, IDAO<EmploymentStatusDetailDTO, EMPLOYMENT_STATUS>
    {
        public bool Delete(EMPLOYMENT_STATUS entity)
        {
            try
            {
                EMPLOYMENT_STATUS employmentStatus = db.EMPLOYMENT_STATUS.First(x => x.employmentStatusID == entity.employmentStatusID);
                employmentStatus.isDeleted = true;
                employmentStatus.deletedDate = DateTime.Today;
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
                EMPLOYMENT_STATUS empStatus = db.EMPLOYMENT_STATUS.First(x=>x.employmentStatusID == ID);
                empStatus.isDeleted = false;
                empStatus.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EMPLOYMENT_STATUS entity)
        {
            try
            {
                db.EMPLOYMENT_STATUS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmploymentStatusDetailDTO> Select()
        {
            try
            {
                List<EmploymentStatusDetailDTO> employmentStatuses = new List<EmploymentStatusDetailDTO>();
                var list = db.EMPLOYMENT_STATUS.Where(x => x.isDeleted == false).OrderBy(x => x.employmentStatus).ToList();
                foreach (var item in list)
                {
                    EmploymentStatusDetailDTO dto = new EmploymentStatusDetailDTO();
                    dto.EmploymentStatusID = item.employmentStatusID;
                    dto.EmploymentStatus = item.employmentStatus;
                    employmentStatuses.Add(dto);
                }
                return employmentStatuses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EmploymentStatusDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<EmploymentStatusDetailDTO> employmentStatuses = new List<EmploymentStatusDetailDTO>();
                var list = db.EMPLOYMENT_STATUS.Where(x => x.isDeleted == isDeleted).OrderBy(x => x.employmentStatus).ToList();
                foreach (var item in list)
                {
                    EmploymentStatusDetailDTO dto = new EmploymentStatusDetailDTO();
                    dto.EmploymentStatusID = item.employmentStatusID;
                    dto.EmploymentStatus = item.employmentStatus;
                    employmentStatuses.Add(dto);
                }
                return employmentStatuses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EMPLOYMENT_STATUS entity)
        {
            try
            {
                EMPLOYMENT_STATUS employmentStatus = db.EMPLOYMENT_STATUS.First(x => x.employmentStatusID == entity.employmentStatusID);
                employmentStatus.employmentStatus = entity.employmentStatus;
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
