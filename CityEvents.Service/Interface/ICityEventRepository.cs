using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface ICityEventRepository
    {
        public  Task<bool> AddEvent(CityEventEntity cityEvent);
        public  Task<bool> EditEvent(CityEventEntity cityEvent, int eventId);
        public  Task<bool> DeleteEvent(int eventId);
        public  Task<CityEventEntity> SearchTitle(string title);
        public  Task<CityEventEntity> SearchEvent(string local, DateTime date);
        public  Task<CityEventEntity> SearchEvent(decimal minPrice, decimal maxPrice, DateTime date);

    }
}
