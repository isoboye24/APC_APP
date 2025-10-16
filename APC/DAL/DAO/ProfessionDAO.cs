using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class ProfessionDAO : APCContexts, IDAO<ProfessionDetailDTO, PROFESSION>
    {
        public bool Delete(PROFESSION entity)
        {
            try
            {
                PROFESSION profession = db.PROFESSION.First(x => x.professionID == entity.professionID);
                profession.isDeleted = true;
                profession.deletedDate = DateTime.Today;
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
            PROFESSION profession = db.PROFESSION.First(x=>x.professionID == ID);
            profession.isDeleted = false;
            profession.deletedDate = null;
            db.SaveChanges();
            return true;
        }

        public bool Insert(PROFESSION entity)
        {
            try
            {
                db.PROFESSION.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfessionDetailDTO> Select()
        {
            try
            {
                List<ProfessionDetailDTO> professions = new List<ProfessionDetailDTO>();
                var list = db.PROFESSION.Where(x => x.isDeleted == false).OrderBy(x => x.profession1).ToList();
                foreach (var item in list)
                {
                    ProfessionDetailDTO dto = new ProfessionDetailDTO();
                    dto.ProfessionID = item.professionID;
                    dto.Profession = item.profession1;
                    professions.Add(dto);
                }
                return professions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProfessionDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<ProfessionDetailDTO> professions = new List<ProfessionDetailDTO>();
                var list = db.PROFESSION.Where(x => x.isDeleted == isDeleted).OrderBy(x => x.profession1).ToList();
                foreach (var item in list)
                {
                    ProfessionDetailDTO dto = new ProfessionDetailDTO();
                    dto.ProfessionID = item.professionID;
                    dto.Profession = item.profession1;
                    professions.Add(dto);
                }
                return professions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count()
        {
            try
            {
                int numberOfProfession = db.PROFESSION.Count(x => x.isDeleted == false);
                return numberOfProfession;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool Update(PROFESSION entity)
        {
            try
            {
                PROFESSION profession = db.PROFESSION.First(x => x.professionID == entity.professionID);
                profession.profession1 = entity.profession1;
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
