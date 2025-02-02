using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class DualNationalityDAO : APCContexts, IDAO<DualNationalityDetailDTO, DUAL_NATIONALITY>
    {
        public bool Delete(DUAL_NATIONALITY entity)
        {
            try
            {
                DUAL_NATIONALITY dualNationality = db.DUAL_NATIONALITY.First(x=>x.dualNationalityID == entity.dualNationalityID);
                dualNationality.isDeleted = true;
                dualNationality.deletedDate = DateTime.Today;
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

        public bool Insert(DUAL_NATIONALITY entity)
        {
            try
            {
                db.DUAL_NATIONALITY.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DualNationalityDetailDTO> Select()
        {
            try
            {
                List<DualNationalityDetailDTO> dualNationalities = new List<DualNationalityDetailDTO>();
                var list = db.DUAL_NATIONALITY.Where(x => x.isDeleted == false).OrderBy(x => x.dualNationality).ToList();
                foreach (var item in list)
                {
                    DualNationalityDetailDTO dto = new DualNationalityDetailDTO();
                    dto.DualNationalityID = item.dualNationalityID;
                    dto.DualNationalityName = item.dualNationality;
                    dualNationalities.Add(dto);
                }
                return dualNationalities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(DUAL_NATIONALITY entity)
        {
            try
            {
                DUAL_NATIONALITY dualNationality = db.DUAL_NATIONALITY.First(x => x.dualNationalityID == entity.dualNationalityID);
                dualNationality.dualNationality = entity.dualNationality;
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
