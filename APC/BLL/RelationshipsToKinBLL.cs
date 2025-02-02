using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class RelationshipsToKinBLL
    {
        RelationshipToNextOfKinDAO dao = new RelationshipToNextOfKinDAO();
        public RelationshipToNextOfKinDTO Select()
        {
            RelationshipToNextOfKinDTO dto = new RelationshipToNextOfKinDTO();
            dto.RelationshipsToKin = dao.Select();
            return dto;
        }
    }
}
