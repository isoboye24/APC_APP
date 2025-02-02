using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class MonthBLL
    {
        MonthDAO dao = new MonthDAO();
        public MonthDTO Select()
        {
            MonthDTO dto = new MonthDTO();
            dto.Months = dao.Select();
            return dto;
        }
    }
}
