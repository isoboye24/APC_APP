using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class AttendanceStatusService : IAttendanceStatusService
    {
        private readonly IAttendanceStatusRepository _repository;
        public AttendanceStatusService(IAttendanceStatusRepository repository)
        {
            _repository = repository;
        }
        public List<AttendanceStatus> GetAll()
            => _repository.GetAll();
    }
}
