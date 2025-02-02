using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.DAL.DAO
{
    public class DocumentDAO : APCContexts, IDAO<DocumentDetailDTO, DOCUMENT>
    {
        public bool Delete(DOCUMENT entity)
        {
            try
            {
                DOCUMENT document = db.DOCUMENTs.First(x=>x.documentID==entity.documentID);
                document.isDeleted = true;
                document.deletedDate = DateTime.Today;
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
                DOCUMENT document = db.DOCUMENTs.First(x=>x.documentID==ID);
                document.isDeleted = false;
                document.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(DOCUMENT entity)
        {
            try
            {
                db.DOCUMENTs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentDetailDTO> Select()
        {
            try
            {
                List<DocumentDetailDTO> documents = new List<DocumentDetailDTO>();
                var list = (from d in db.DOCUMENTs.Where(x => x.isDeleted == false)
                            join m in db.MONTHs on d.monthID equals m.monthID
                            select new
                            {
                                documentID = d.documentID,
                                documentName = d.documentName,
                                documentType = d.documentType,
                                day = d.day,
                                monthID = d.monthID,
                                monthName = m.monthName,
                                year = d.year,
                                documentPath = d.documentPath
                            }).OrderByDescending(x=>x.year).ThenByDescending(x=>x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.documentName).ToList();
                foreach (var item in list)
                {
                    DocumentDetailDTO dto = new DocumentDetailDTO();
                    dto.DocumentID = item.documentID;
                    dto.DocumentName = item.documentName;
                    dto.DocumentPath = item.documentPath;
                    dto.DocumentType = item.documentType;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.Date = item.day + "."+ item.monthID + "."+ item.year;
                    documents.Add(dto);
                }
                return documents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<DocumentDetailDTO> documents = new List<DocumentDetailDTO>();
                var list = (from d in db.DOCUMENTs.Where(x => x.isDeleted == isDeleted)
                            join m in db.MONTHs on d.monthID equals m.monthID
                            select new
                            {
                                documentID = d.documentID,
                                documentName = d.documentName,
                                documentType = d.documentType,
                                day = d.day,
                                monthID = d.monthID,
                                monthName = m.monthName,
                                year = d.year,
                                documentPath = d.documentPath
                            }).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).ThenBy(x => x.documentName).ToList();
                foreach (var item in list)
                {
                    DocumentDetailDTO dto = new DocumentDetailDTO();
                    dto.DocumentID = item.documentID;
                    dto.DocumentName = item.documentName;
                    dto.DocumentPath = item.documentPath;
                    dto.DocumentType = item.documentType;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.Date = item.day + "." + item.monthID + "." + item.year;
                    documents.Add(dto);
                }
                return documents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectDocCount()
        {
            try
            {
                int counter = db.DOCUMENTs.Count(x => x.isDeleted == false);                
                return counter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(DOCUMENT entity)
        {
            try
            {
                DOCUMENT document = db.DOCUMENTs.First(x=>x.documentID==entity.documentID);
                document.documentName = entity.documentName;
                document.documentPath = entity.documentPath;
                document.documentType = entity.documentType;
                document.day = entity.day;
                document.monthID = entity.monthID;
                document.year = entity.year;
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
