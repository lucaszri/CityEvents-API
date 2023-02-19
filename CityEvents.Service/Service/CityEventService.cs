using AutoMapper;
using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Service
{
    public class CityEventService : ICityEventService
    {
        private ICityEventRepository _repository;
        private IMapper _mapper;

        public CityEventService(ICityEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddEvent(CityEventDto cityEvent)
        {
            CityEventEntity entity = _mapper.Map<CityEventEntity>(cityEvent);
            return await _repository.AddEvent(entity);
        }
        public async Task<bool> EditEvent(CityEventDto cityEvent, int eventId)
        {
            CityEventEntity entity = _mapper.Map<CityEventEntity>(cityEvent);
            return await _repository.EditEvent(entity, eventId);
        }
        public async Task<bool> DeleteEvent(int eventId)
        {
            return await _repository.DeleteEvent(eventId);
        }
        public async Task<CityEventDto> SearchTitle(string title)
        {
            CityEventEntity entity = await _repository.SearchTitle(title);
            CityEventDto cityEvent = _mapper.Map<CityEventDto>(entity);
            return cityEvent;
        }
        public async Task<CityEventDto> SearchEvent(string local, DateTime date)
        {
            CityEventEntity entity = await _repository.SearchEvent(local, date);
            CityEventDto cityEvent = _mapper.Map<CityEventDto>(entity);
            return cityEvent;
        }
        public async Task<CityEventDto> SearchEvent(decimal minPrice, decimal maxPrice, DateTime date)
        {
            CityEventEntity entity = await _repository.SearchEvent(minPrice, maxPrice, date);
            CityEventDto cityEvent = _mapper.Map<CityEventDto>(entity);
            return cityEvent;
        }
    }
}
