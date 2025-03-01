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
    public class ConstitutionBLL : IBLL<ConstitutionDTO, ConstitutionDetailDTO>
    {
        ConstitutionDAO dao = new ConstitutionDAO();
        public bool Delete(ConstitutionDetailDTO entity)
        {
            try
            {
                CONSTITUTION constit = new CONSTITUTION();
                constit.constitutionID = entity.ConstitutionID;
                return dao.Delete(constit);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool GetBack(ConstitutionDetailDTO entity)
        {
            return dao.GetBack(entity.ConstitutionID);
        }

        public bool Insert(ConstitutionDetailDTO entity)
        {
            CONSTITUTION constit = new CONSTITUTION();
            constit.constitution1 = entity.ConstitutionText;
            constit.fine = entity.Fine;
            constit.section = entity.Section;
            constit.ShortDescription = entity.ShortDescription;
            return dao.Insert(constit);
        }

        public ConstitutionDTO Select()
        {
            ConstitutionDTO dto = new ConstitutionDTO();
            dto.Constitutions = dao.Select();
            return dto;
        }

        public List<CONSTITUTION> GetSingleConstitution(int ID)
        {
            return dao.GetSingleConstitution(ID);
        }

        public bool Update(ConstitutionDetailDTO entity)
        {
            CONSTITUTION constit = new CONSTITUTION();
            constit.constitutionID = entity.ConstitutionID;
            constit.constitution1 = entity.ConstitutionText;
            constit.fine = entity.Fine;
            constit.section = entity.Section;
            constit.ShortDescription = entity.ShortDescription;
            return dao.Update(constit);
        }
    }
}
