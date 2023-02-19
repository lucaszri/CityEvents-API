using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface ICityEventService
    {
        public Task<bool> AddEvent(CityEventDto cityEvent);
        public  Task<bool> EditEvent(CityEventDto cityEvent, int eventId);
        public Task<bool> DeleteEvent(int eventId);
        public Task<CityEventDto> SearchTitle(string title);
        public Task<CityEventDto> SearchEvent(string local, DateTime date);
        public Task<CityEventDto> SearchEvent(decimal minPrice, decimal maxPrice, DateTime date);

    }
}
