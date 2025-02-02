using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class AttendanceStatusDAO:APCContexts
    {
        public List<AttendanceStatusDetailDTO> Select()
        {
			try
			{
                List<AttendanceStatusDetailDTO> attendanceStatuses = new List<AttendanceStatusDetailDTO>();
                var list = db.ATTENDANCE_STATUS.Where(x => x.isDeleted == false).ToList();
                foreach (var item in list)
                {
                    AttendanceStatusDetailDTO dto = new AttendanceStatusDetailDTO();
                    dto.AttendanceStatusID = item.attendanceStatusID;
                    dto.AttendanceStatusName = item.attendanceStatus;
                    attendanceStatuses.Add(dto);
                }
                return attendanceStatuses;
            }
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}
