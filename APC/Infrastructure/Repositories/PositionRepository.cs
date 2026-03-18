using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly APCEntities _db;
        public PositionRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.POSITION.Count(x => !x.isDeleted);
        }

        public bool GetBack(int ID)
        {
            var entity = _db.POSITION.First(x => x.positionID == ID);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _db.POSITION.First(x => x.positionID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.POSITION.Any(x => !x.isDeleted && x.positionName == name);
        }

        public IQueryable<POSITION> GetAll()
        {
            return _db.POSITION.Where(x => !x.isDeleted);
        }

        public Position GetById(int id)
        {
            var entity = _db.POSITION.FirstOrDefault(x => x.positionID == id && !x.isDeleted);
            if (entity == null) return null;

            var position = new Position(entity.positionName);
            position.SetId(entity.positionID);
            return position;
        }

        public bool Insert(Position position)
        {
            _db.POSITION.Add(new POSITION
            {
                positionName = position.PositionName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.POSITION.FirstOrDefault(x => x.positionID == id);

            if (entity == null)
                return false;

            _db.POSITION.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Position position)
        {
            var entity = _db.POSITION.First(x => x.positionID == position.PositionId);
            entity.positionName = position.PositionName;
            _db.SaveChanges();
            return true;
        }
    }
}
