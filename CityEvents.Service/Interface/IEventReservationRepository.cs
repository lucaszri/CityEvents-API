using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CityEvents.Service.Entity;

namespace CityEvents.Service.Interface

{
    public interface IEventReservationRepository
    {
        public  Task<bool> AddReservation(EventReservationEntity eventReservationEntity);
        public  Task<bool> EditReservation(int quantity, int idReservation);
        public  Task<bool> DeleteReservation(int idReservation);
        public  Task<EventReservationEntity> SearchReservation(string personName, string title);
    }
}
