using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface IEventReservationService
    {
        public  Task<bool> AddReservation(EventReservationDto eventReservation);
        public  Task<bool> EditReservation(int quantity, int idReservation);
        public  Task<bool> DeleteReservation(int idReservation);
        public  Task<EventReservationDto> SearchReservation(string personName, string title);
    }
}
