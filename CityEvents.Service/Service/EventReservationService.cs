using AutoMapper;
using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Service
{
    public class EventReservationService : IEventReservationService
    {
        private IEventReservationRepository _repository;
        private IMapper _mapper;

        public EventReservationService (IEventReservationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddReservation(EventReservationDto eventReservation)
        {
            EventReservationEntity entity  = _mapper.Map<EventReservationEntity>(eventReservation);
            return await _repository.AddReservation(entity);
        }
        public async Task<bool> EditReservation(int quantity, int idReservation)
        {
            quantity = _mapper.Map<int>(quantity);
            return await _repository.EditReservation(quantity, idReservation);
        }
        public async Task<bool> DeleteReservation(int idReservation)
        {
            return await _repository.DeleteReservation(idReservation);
        }
        public async Task<EventReservationDto> SearchReservation(string personName, string title)
        {
            EventReservationEntity entity = await _repository.SearchReservation(personName, title);
            EventReservationDto eventReservation = _mapper.Map<EventReservationDto>(entity);
            return eventReservation;
        }
    }
}
