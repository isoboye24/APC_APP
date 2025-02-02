using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class EventImageDAO : APCContexts, IDAO<EventImageDetailDTO, EVENT_IMAGE>
    {
        public bool Delete(EVENT_IMAGE entity)
        {
            try
            {
                EVENT_IMAGE eventImage = db.EVENT_IMAGE.First(x=>x.eventImageID==entity.eventImageID);
                eventImage.isDeleted = true;
                eventImage.deletedDate = DateTime.Today;
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
            try
            {
                EVENT_IMAGE eventImage = db.EVENT_IMAGE.First(x=>x.eventImageID==ID);
                eventImage.isDeleted = false;
                eventImage.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EVENT_IMAGE entity)
        {
            try
            {
                db.EVENT_IMAGE.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EventImageDetailDTO> Select()
        {
            throw new NotImplementedException();
        }
        public List<EventImageDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<EventImageDetailDTO> eventImages = new List<EventImageDetailDTO>();
                int counter = 0;
                var list = (from ei in db.EVENT_IMAGE.Where(x => x.isDeleted == isDeleted)
                            join e in db.EVENTS on ei.eventID equals e.eventID
                            select new
                            {
                                eventImageID = ei.eventImageID,
                                eventID = ei.eventID,
                                eventTitle = e.title,
                                year = e.year,
                                summary = ei.summary,
                                imagePath = ei.imagePath,
                                imageCaption = ei.imageCaption,
                                isEventDeleted = e.isDeleted,
                            }).ToList();
                foreach (var item in list)
                {
                    EventImageDetailDTO dto = new EventImageDetailDTO();
                    dto.EventImageID = item.eventImageID;
                    dto.EventID = item.eventID;
                    dto.EventTitle = item.eventTitle;
                    dto.EventYear = item.year;
                    dto.Summary = item.summary;
                    dto.ImagePath = item.imagePath;
                    dto.ImageCaption = item.imageCaption;
                    dto.isEventDeleted = item.isEventDeleted;
                    dto.Counter = ++counter;
                    eventImages.Add(dto);
                }
                return eventImages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventImageDetailDTO> SelectSpecificImage(int ID)
        {
            try
            {
                List<EventImageDetailDTO> eventImages = new List<EventImageDetailDTO>();
                int counter = 0;
                var list = (from ei in db.EVENT_IMAGE.Where(x => x.isDeleted == false && x.eventID==ID)
                            join e in db.EVENTS.Where(x => x.isDeleted == false) on ei.eventID equals e.eventID
                            select new 
                            {
                                eventImageID = ei.eventImageID,
                                eventID = ei.eventID,
                                eventTitle = e.title,
                                year = e.year,
                                summary = ei.summary,
                                imagePath = ei.imagePath,
                                imageCaption = ei.imageCaption
                            }).ToList();
                foreach (var item in list)
                {
                    EventImageDetailDTO dto = new EventImageDetailDTO();
                    dto.EventImageID = item.eventImageID;
                    dto.EventID = item.eventID;
                    dto.EventTitle = item.eventTitle;
                    dto.EventYear = item.year;
                    dto.Summary = item.summary;
                    dto.ImagePath = item.imagePath;
                    dto.ImageCaption = item.imageCaption;
                    dto.Counter = ++counter;
                    eventImages.Add(dto);
                }
                return eventImages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EVENT_IMAGE entity)
        {
            try
            {
                EVENT_IMAGE eventImage = db.EVENT_IMAGE.First(x=>x.eventImageID==entity.eventImageID);
                eventImage.eventID = entity.eventID;
                eventImage.summary = entity.summary;
                eventImage.imageCaption = entity.imageCaption;
                eventImage.imagePath = entity.imagePath;
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
