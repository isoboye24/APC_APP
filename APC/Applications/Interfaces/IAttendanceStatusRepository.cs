using APC.Infrastructure.Data;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IAttendanceStatusRepository
    {
        IQueryable<ATTENDANCE_STATUS> GetAll();
        IQueryable<ATTENDANCE_STATUS> GetById(int id);
        IQueryable<ATTENDANCE_STATUS> GetByStatus(string status);
    }
}
