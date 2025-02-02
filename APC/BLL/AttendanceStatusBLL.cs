using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APC.DAL.DAO;
using APC.DAL.DTO;

namespace APC.BLL
{
    public class AttendanceStatusBLL
    {
        AttendanceStatusDAO dao = new AttendanceStatusDAO();
        public AttendanceStatusDTO Select()
        {
            AttendanceStatusDTO dto = new AttendanceStatusDTO();
            dto.AttendanceStatuses = dao.Select();
            return dto;
        }
    }
}
