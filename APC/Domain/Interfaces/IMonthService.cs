using APC.Applications.DTO;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IMonthService
    {
        List<MonthDTO> GetAll();
    }
}
