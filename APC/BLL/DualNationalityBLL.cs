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
    public class DualNationalityBLL : IBLL<DualNationalityDTO, DualNationalityDetailDTO>
    {
        DualNationalityDAO dao = new DualNationalityDAO();
        public bool Delete(DualNationalityDetailDTO entity)
        {
            DUAL_NATIONALITY dualNationality = new DUAL_NATIONALITY();
            dualNationality.dualNationalityID = entity.DualNationalityID;
            return dao.Delete(dualNationality);
        }

        public bool GetBack(DualNationalityDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(DualNationalityDetailDTO entity)
        {
            DUAL_NATIONALITY dualNationality = new DUAL_NATIONALITY();
            dualNationality.dualNationality = entity.DualNationalityName;            
            return dao.Insert(dualNationality);
        }

        public DualNationalityDTO Select()
        {
            throw new NotImplementedException();
        }

        public bool Update(DualNationalityDetailDTO entity)
        {
            DUAL_NATIONALITY dualNationality = new DUAL_NATIONALITY();
            dualNationality.dualNationalityID = entity.DualNationalityID;
            dualNationality.dualNationality = entity.DualNationalityName;            
            return dao.Update(dualNationality); ;
        }
    }
}
