using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class AttendanceStatus
    {
        public int AttendanceStatusId { get; private set; }
        public string AttendanceStatusName { get; private set; }

        public AttendanceStatus(string name)
        {
            AttendanceStatusName = name;
        }

        public static AttendanceStatus Rehydrate(int id, string name)
        {
            return new AttendanceStatus(name)
            {
                AttendanceStatusId = id
            };
        }

        public void SetId(int id)
        {
            AttendanceStatusId = id;
        }
    }
}
