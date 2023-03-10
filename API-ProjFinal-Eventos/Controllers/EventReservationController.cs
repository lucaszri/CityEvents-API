using API_ProjFinal_Eventos.Filters;
using CityEvents.Service.Dto;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ProjFinal_Eventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : Controller
    {
        private IEventReservationService _eventReservation;

        public EventReservationController (IEventReservationService eventReservation)
        {
            _eventReservation = eventReservation;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventReservationDto>> SearchReservation(string personName, string title)
        {
            return Ok(await _eventReservation.SearchReservation(personName, title));
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddReservation(EventReservationDto eventReservation)
        {
           if(!await _eventReservation.AddReservation(eventReservation))
            {
                return BadRequest();
            }
           return CreatedAtAction(nameof(AddReservation), eventReservation);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> EditReservation(int quantity, int idReservation)
        {
            if(!await _eventReservation.EditReservation(quantity, idReservation))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteReservation(int idReservation)
        {
            if(await _eventReservation.DeleteReservation(idReservation))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
