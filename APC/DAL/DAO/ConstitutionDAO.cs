using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class ConstitutionDAO : APCContexts, IDAO<ConstitutionDetailDTO, CONSTITUTION>
    {
        public bool Delete(CONSTITUTION entity)
        {
            try
            {
                CONSTITUTION constit = db.CONSTITUTION.First(x => x.constitutionID == entity.constitutionID);
                constit.isDeleted = true;
                constit.deletedDate = DateTime.Today;
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
                CONSTITUTION constit = db.CONSTITUTION.First(x => x.constitutionID == ID);
                constit.isDeleted = false;
                constit.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(CONSTITUTION entity)
        {
            try
            {
                db.CONSTITUTION.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConstitutionDetailDTO> Select()
        {
            try
            {
                List<ConstitutionDetailDTO> constitutions = new List<ConstitutionDetailDTO>();
                var list = db.CONSTITUTION.Where(x => x.isDeleted == false).ToList();
                foreach (var item in list)
                {
                    ConstitutionDetailDTO dto = new ConstitutionDetailDTO();
                    dto.ConstitutionID = item.constitutionID;
                    dto.ConstitutionText = item.constitution1;
                    if (item.ShortDescription.Length == 0 || item.ShortDescription == null)
                    {
                        dto.ShortDescription = "No Description";
                    }
                    else
                    {
                        dto.ShortDescription = item.ShortDescription;
                    }
                    dto.Section = item.section;
                    dto.Fine = item.fine;
                    dto.FineWithCurrency = "€ " + item.fine;
                    constitutions.Add(dto);
                }
                return constitutions;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public List<ConstitutionDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<ConstitutionDetailDTO> constitutions = new List<ConstitutionDetailDTO>();
                var list = db.CONSTITUTION.Where(x => x.isDeleted == isDeleted).ToList();
                foreach (var item in list)
                {
                    ConstitutionDetailDTO dto = new ConstitutionDetailDTO();
                    dto.ConstitutionID = item.constitutionID;
                    dto.Section = item.section;
                    dto.ShortDescription = item.ShortDescription;
                    dto.ConstitutionText = item.constitution1;
                    dto.Fine = item.fine;
                    dto.FineWithCurrency = "€ " + item.fine;
                    constitutions.Add(dto);
                }
                return constitutions;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<CONSTITUTION> GetSingleConstitution(int ID)
        {
            try
            {
                List<CONSTITUTION> list = db.CONSTITUTION.Where(x => x.constitutionID == ID).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConstitutionSectionDetailDTO> SelectConstitutionSections()
        {
            try
            {
                List<ConstitutionSectionDetailDTO> constitutionSections = new List<ConstitutionSectionDetailDTO>();
                var list = db.CONSTITUTION.Where(x => x.isDeleted == false).ToList();
                foreach (var item in list)
                {
                    ConstitutionSectionDetailDTO dto = new ConstitutionSectionDetailDTO();
                    dto.ConstitutionID = item.constitutionID;
                    dto.Section = item.section;
                    constitutionSections.Add(dto);
                }
                return constitutionSections;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(CONSTITUTION entity)
        {
            try
            {
                CONSTITUTION constit = new CONSTITUTION();
                constit = db.CONSTITUTION.First(x => x.constitutionID == entity.constitutionID);
                constit.constitution1 = entity.constitution1;
                constit.fine = entity.fine;
                constit.section = entity.section;
                constit.ShortDescription = entity.ShortDescription;
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
