﻿using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class EventsDAO : APCContexts, IDAO<EventsDetailDTO, EVENT>
    {
        public bool Delete(EVENT entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            try
            {
                EVENT events = db.EVENTS.First(x=>x.eventID==ID);
                events.isDeleted = false;
                events.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EVENT entity)
        {
            try
            {
                db.EVENTS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventsDetailDTO> Select()
        {
            try
            {
                List<EventsDetailDTO> events = new List<EventsDetailDTO>();
                var list = (from e in db.EVENTS.Where(x => x.isDeleted == false)
                            join m in db.MONTHs on e.monthID equals m.monthID
                            select new
                            {
                                eventID = e.eventID,
                                eventTitle = e.title,
                                summary = e.summary,
                                coverImagePath = e.coverImagePath,
                                day = e.day,
                                monthID = e.monthID,
                                monthName = m.monthName,
                                year = e.year,
                                eventDate = e.eventDate,
                            }).OrderByDescending(x => x.year).ToList();
                foreach (var item in list)
                {
                    EventsDetailDTO dto = new EventsDetailDTO();
                    dto.EventID = item.eventID;
                    dto.EventTitle = item.eventTitle;
                    dto.Summary = item.summary;
                    dto.CoverImagePath = item.coverImagePath;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.EventDate = item.eventDate;
                    events.Add(dto);
                }
                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SelectRecentEvent()
        {
            try
            {
                int eventCount = db.EVENTS.Count(x=>x.isDeleted == false);
                if (eventCount > 0)
                {
                    var recentEvent = (from e in db.EVENTS.Where(x => x.isDeleted == false)
                                       join m in db.MONTHs on e.monthID equals m.monthID
                                       select new
                                       {
                                           eventID = e.eventID,
                                           eventTitle = e.title,
                                           summary = e.summary,
                                           coverImagePath = e.coverImagePath,
                                           day = e.day,
                                           monthID = e.monthID,
                                           monthName = m.monthName,
                                           year = e.year
                                       }).OrderByDescending(x => x.year).ThenByDescending(x=>x.monthID).ThenByDescending(x => x.day).FirstOrDefault();
                    List<EventsDetailDTO> dto = new List<EventsDetailDTO>();

                    return recentEvent.day + "." + recentEvent.monthID + "." + recentEvent.year;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectEventCount()
        {
            try
            {
                int totalEvents = db.EVENTS.Count(x => x.isDeleted == false);
                return totalEvents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EventsDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<EventsDetailDTO> events = new List<EventsDetailDTO>();
                var list = (from e in db.EVENTS.Where(x => x.isDeleted == isDeleted)
                            join m in db.MONTHs on e.monthID equals m.monthID
                            select new
                            {
                                eventID = e.eventID,
                                eventTitle = e.title,
                                summary = e.summary,
                                coverImagePath = e.coverImagePath,
                                day = e.day,
                                monthID = e.monthID,
                                monthName = m.monthName,
                                year = e.year,
                                eventDate = e.eventDate
                            }).OrderByDescending(x => x.year).ToList();
                foreach (var item in list)
                {
                    EventsDetailDTO dto = new EventsDetailDTO();
                    dto.EventID = item.eventID;
                    dto.EventTitle = item.eventTitle;
                    dto.Summary = item.summary;
                    dto.CoverImagePath = item.coverImagePath;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.EventDate = item.eventDate;
                    events.Add(dto);
                }
                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EVENT entity)
        {
            try
            {
                EVENT events = db.EVENTS.First(x => x.eventID == entity.eventID);
                events.summary = entity.summary;
                events.title = entity.title;
                events.coverImagePath = entity.coverImagePath;
                events.day = entity.day;
                events.monthID = entity.monthID;
                events.year = entity.year;
                events.eventDate = entity.eventDate;
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
