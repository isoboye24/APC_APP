using APC.Applications.DTO;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IMonthService
    {
        List<MonthDTO> GetAll();
    }
}
