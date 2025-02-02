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
    public class DocumentBLL : IBLL<DocumentDTO, DocumentDetailDTO>
    {
        DocumentDAO dao = new DocumentDAO();
        MonthDAO monthDAO = new MonthDAO();
        public bool Delete(DocumentDetailDTO entity)
        {
            DOCUMENT document = new DOCUMENT();
            document.documentID = entity.DocumentID;
            return dao.Delete(document);
        }

        public bool GetBack(DocumentDetailDTO entity)
        {
            return dao.GetBack(entity.DocumentID);
        }

        public bool Insert(DocumentDetailDTO entity)
        {
            DOCUMENT document = new DOCUMENT();
            document.documentName = entity.DocumentName;
            document.documentPath = entity.DocumentPath;
            document.documentType = entity.DocumentType;
            document.day = entity.Day;
            document.monthID = entity.MonthID;
            document.year = Convert.ToInt32(entity.Year);
            return dao.Insert(document);
        }

        public DocumentDTO Select()
        {
            DocumentDTO dto = new DocumentDTO();
            dto.Documents = dao.Select();
            dto.Months = monthDAO.Select();
            return dto;
        }
        public int SelectDocCount()
        {
            return dao.SelectDocCount();
        }

        public bool Update(DocumentDetailDTO entity)
        {
            DOCUMENT document = new DOCUMENT();
            document.documentID = entity.DocumentID;
            document.documentName = entity.DocumentName;
            document.documentPath = entity.DocumentPath;
            document.documentType = entity.DocumentType;
            document.day = entity.Day;
            document.monthID = entity.MonthID;
            document.year = Convert.ToInt32(entity.Year);
            return dao.Update(document);
        }
    }
}
