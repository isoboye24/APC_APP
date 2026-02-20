using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class EventImageBLL : IBLL<EventImageDTO, EventImageDetailDTO>
    {
        EventImageDAO dao = new EventImageDAO();
        EventsDAO eventDAO = new EventsDAO();
        public bool Delete(EventImageDetailDTO entity)
        {
            EVENT_IMAGE eventImage = new EVENT_IMAGE();
            eventImage.eventImageID = entity.EventImageID;
            return dao.Delete(eventImage);
        }

        public bool GetBack(EventImageDetailDTO entity)
        {
            return dao.GetBack(entity.EventImageID);            
        }

        public bool Insert(EventImageDetailDTO entity)
        {
            EVENT_IMAGE eventImage = new EVENT_IMAGE();
            eventImage.eventID = entity.EventID;
            eventImage.summary = entity.Summary;
            eventImage.imageCaption = entity.ImageCaption;
            eventImage.imagePath = entity.ImagePath;
            return dao.Insert(eventImage);
        }

        public EventImageDTO Select()
        {
            throw new NotImplementedException();
        }
        public EventImageDTO Select(int eventID)
        {
            EventImageDTO dto = new EventImageDTO();
            dto.EventImages = dao.Select(eventID);
            return dto;
        }
        
        public EventImageDTO Select(bool isDeleted)
        {
            EventImageDTO dto = new EventImageDTO();
            dto.EventImages = dao.Select(isDeleted);
            return dto;
        }

        public bool Update(EventImageDetailDTO entity)
        {
            EVENT_IMAGE eventImage = new EVENT_IMAGE();
            eventImage.eventImageID = entity.EventImageID;
            eventImage.eventID = entity.EventID;
            eventImage.summary = entity.Summary;
            eventImage.imageCaption = entity.ImageCaption;
            eventImage.imagePath = entity.ImagePath;
            return dao.Update(eventImage);
        }
    }
}
