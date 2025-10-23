using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class MembersCommittmentBLL
    {
        MembersCommittmentDAO dao = new MembersCommittmentDAO();
        public MembersCommittmentDTO Select(int year)
        {
            MembersCommittmentDTO dto = new MembersCommittmentDTO();
            dto.Committments = dao.Select(year);
            return dto;
        }
    }
}
