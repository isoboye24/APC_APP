using APC.Applications.DTO;
using APC.Applications.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class AttendanceStatusService : IAttendanceStatusService
    {
        private readonly IAttendanceStatusRepository _repository;
        public AttendanceStatusService(IAttendanceStatusRepository repository)
        {
            _repository = repository;
        }
        public List<AttendanceStatusDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new AttendanceStatusDTO
                {
                    AttendanceStatusId = x.attendanceStatusID,
                    AttendanceStatusName = x.attendanceStatus
                })
                .OrderBy(x => x.AttendanceStatusName)
                .ToList();
        }
        
        public List<AttendanceStatusDTO> GetById(int id)
        {
            return _repository.GetById(id)
                .Select(x => new AttendanceStatusDTO
                {
                    AttendanceStatusId = x.attendanceStatusID,
                    AttendanceStatusName = x.attendanceStatus
                })
                .OrderBy(x => x.AttendanceStatusName)
                .ToList();
        }

    }
}
