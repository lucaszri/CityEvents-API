using CityEvents.Service.Dto;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityEventDto>> SearchTitle(string title)
        {
            return Ok(await _cityEvent.SearchTitle(title));
        }

        [HttpGet("SearchLocalAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityEventDto>> SearchEvent(string local, DateTime date)
        {
            return Ok(await _cityEvent.SearchEvent(local, date));
        }

        [HttpGet("SearchPriceAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityEventDto>> SearchEvent(decimal minPrice, decimal maxPrice, DateTime date)
        {
            return Ok(await _cityEvent.SearchEvent(minPrice, maxPrice, date));
        }

        [HttpPost]
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
