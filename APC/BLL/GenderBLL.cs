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
    public class GenderBLL
    {
        GenderDAO dao = new GenderDAO();
        
        public GenderDTO Select()
        {
            GenderDTO dto = new GenderDTO();
            dto.Genders = dao.Select();
            return dto;
        }
    }
}
