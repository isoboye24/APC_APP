using APC.Applications.DTO;
using APC.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _repository;
        public GenderService(IGenderRepository repository)
        {
            _repository = repository;
        }

        public List<GenderDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new GenderDTO
                {
                    GenderId = x.genderID,
                    GenderName = x.genderName
                })
                .OrderBy(x => x.GenderName)
                .ToList();
        }
    }
}
