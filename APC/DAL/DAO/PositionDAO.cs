using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class PositionDAO : APCContexts, IDAO<PositionDetailDTO, POSITION>
    {
        public bool Delete(POSITION entity)
        {
            try
            {
                POSITION position = db.POSITIONs.First(x => x.positionID == entity.positionID);
                position.isDeleted = true;
                position.deletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            POSITION position = db.POSITIONs.First(x => x.positionID == ID);
            position.isDeleted = false;
            position.deletedDate = null;
            db.SaveChanges();
            return true;
        }

        public bool Insert(POSITION entity)
        {
            try
            {
                db.POSITIONs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PositionDetailDTO> Select()
        {
            try
            {
                List<PositionDetailDTO> positions = new List<PositionDetailDTO>();
                var list = db.POSITIONs.Where(x =>x.isDeleted==false).OrderBy(x => x.positionName).ToList();
                foreach (var item in list)
                {
                    PositionDetailDTO dto = new PositionDetailDTO();
                    dto.PositionID = item.positionID;
                    dto.PositionName = item.positionName;
                    positions.Add(dto);
                }
                return positions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PositionDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<PositionDetailDTO> positions = new List<PositionDetailDTO>();
                var list = db.POSITIONs.Where(x => x.isDeleted == isDeleted).OrderBy(x => x.positionName).ToList();
                foreach (var item in list)
                {
                    PositionDetailDTO dto = new PositionDetailDTO();
                    dto.PositionID = item.positionID;
                    dto.PositionName = item.positionName;
                    positions.Add(dto);
                }
                return positions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PositionCount()
        {
            try
            {
                int position = db.POSITIONs.Count(x => x.isDeleted == false);
                return position;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(POSITION entity)
        {
            try
            {
                POSITION position = db.POSITIONs.First(x => x.positionID == entity.positionID);
                position.positionName = entity.positionName;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
