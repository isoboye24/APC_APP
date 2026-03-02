using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _repository;
        public PositionService(IPositionRepository repository)
        {
            _repository = repository;
        }
        public int Count()
            => _repository.Count();

        public bool Create(Position position)
        {
            if (_repository.Exists(position.PositionName))
                throw new Exception("Position already exists");

            return _repository.Insert(position);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<Position> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Position position)
        {
            var check = _repository.GetById(position.PositionId);
            if (check == null)
                throw new Exception("Position not found");

            position.UpdateName(position.PositionName);
            return _repository.Update(position);
        }
    }
}
