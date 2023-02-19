using API_ProjFinal_Eventos.Filters;
using CityEvents.Service.Dto;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_ProjFinal_Eventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventController : ControllerBase
    {
        private ICityEventService _cityEvent;

        public CityEventController (ICityEventService cityEvent)
        {
            _cityEvent = cityEvent;
        }

        [HttpGet("SearchTitle")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityEventDto>> SearchTitle(string title)
        {
            return Ok(await _cityEvent.SearchTitle(title));
        }

        [HttpGet("SearchLocalAndDate")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityEventDto>> SearchEvent(string local, DateTime date)
        {
            return Ok(await _cityEvent.SearchEvent(local, date));
        }

        [HttpGet("SearchPriceAndDate")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityEventDto>> SearchEvent(decimal minPrice, decimal maxPrice, DateTime date)
        {
            return Ok(await _cityEvent.SearchEvent(minPrice, maxPrice, date));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<bool>> AddEvent(CityEventDto cityEvent)
        {
            if(!await _cityEvent.AddEvent(cityEvent))
                {
                    return BadRequest();
                }
            return CreatedAtAction(nameof(SearchTitle), cityEvent);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> EditEvent(CityEventDto cityEvent, int eventId)
        {
            if(!await _cityEvent.EditEvent(cityEvent, eventId))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<bool>> DeleteEvent(int eventId)
        {
            if(!await _cityEvent.DeleteEvent(eventId))
            {
                return BadRequest();
            }
            return NoContent();
        }


    }
}
