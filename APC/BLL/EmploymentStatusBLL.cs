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
    public class EmploymentStatusBLL : IBLL<EmploymentStatusDTO, EmploymentStatusDetailDTO>
    {
        EmploymentStatusDAO dao = new EmploymentStatusDAO();
        public bool Delete(EmploymentStatusDetailDTO entity)
        {
            EMPLOYMENT_STATUS employmentStatus = new EMPLOYMENT_STATUS();
            employmentStatus.employmentStatusID = entity.EmploymentStatusID;
            return dao.Delete(employmentStatus);
        }

        public bool GetBack(EmploymentStatusDetailDTO entity)
        {
            return dao.GetBack(entity.EmploymentStatusID);
        }

        public bool Insert(EmploymentStatusDetailDTO entity)
        {
            EMPLOYMENT_STATUS employmentStatus = new EMPLOYMENT_STATUS();
            employmentStatus.employmentStatus = entity.EmploymentStatus;
            return dao.Insert(employmentStatus);
        }

        public EmploymentStatusDTO Select()
        {
            EmploymentStatusDTO employmentStatuses = new EmploymentStatusDTO();
            employmentStatuses.EmploymentStatuses = dao.Select();
            return employmentStatuses;
        }

        public bool Update(EmploymentStatusDetailDTO entity)
        {
            EMPLOYMENT_STATUS employmentStatus = new EMPLOYMENT_STATUS();
            employmentStatus.employmentStatusID = entity.EmploymentStatusID;
            employmentStatus.employmentStatus = entity.EmploymentStatus;
            return dao.Update(employmentStatus);
        }
    }
}
