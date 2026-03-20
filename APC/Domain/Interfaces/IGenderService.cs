using APC.Applications.DTO;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IGenderService
    {
        List<GenderDTO> GetAll();
    }
}
