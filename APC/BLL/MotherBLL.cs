using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class MotherBLL
    {
        MotherDAO dao = new MotherDAO();
        public MotherDTO Select()
        {
            MotherDTO dto = new MotherDTO();
            dto.Mothers = dao.Select();
            return dto;
        }
    }
}
