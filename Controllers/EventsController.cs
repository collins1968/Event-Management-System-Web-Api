using Assessment_5.Entitites;
using Assessment_5.Requests;
using Assessment_5.Responses;
using Assessment_5.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventsInterface _eventInterface;
        private readonly IMapper _mapper;

        public EventsController(IEventsInterface eventInterface, IMapper mapper)
        {
            _eventInterface = eventInterface;
            _mapper = mapper;
        }

        //add event
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<SuccessMessage>> AddEvent(AddEvent newEvent)
        {
            var events = _mapper.Map<Event>(newEvent);
            var result = await _eventInterface.CreateEvent(events);
            var response = new SuccessMessage(200, result);
            return CreatedAtAction(nameof(AddEvent), response);
        }


        //get all events and filter by location
        [HttpGet]
        public async Task<ActionResult<Event>> GetAllEventsAsync(string? location)
        {
            var result = await _eventInterface.GetAllEvents(location);
            if (result == null)
            {
                return NotFound(new SuccessMessage(404, "Event Does Not Exist"));
            }
            var events = _mapper.Map<IEnumerable<EventResponse>>(result);
            return Ok(events);
        }


        //get event by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventByIdAsync(Guid id)
        {
            var result = await _eventInterface.GetEventById(id);
            if (result == null)
            {
                return NotFound(new SuccessMessage(404, "Event Does Not Exist"));
            }
            return Ok(result);
        }


        //update event
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<SuccessMessage>> UpdateEventAsync(Guid id, AddEvent updateEvent)
        {
            var response = await _eventInterface.GetEventById(id);
            if (response == null)
            {
                return NotFound(new SuccessMessage(404, "Event Does Not Exist"));
            }
            var events = _mapper.Map(updateEvent, response);
            var result = await _eventInterface.UpdateEvent(events);
            return Ok(new SuccessMessage(200, result));
        }

        //delete event
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<SuccessMessage>> DeleteEventAsync(Guid id)
        {
            var response = await _eventInterface.GetEventById(id);
            if (response == null)
            {
                return NotFound(new SuccessMessage(404, "Event Does Not Exist"));
            }
            var result = await _eventInterface.DeleteEvent(response);
            return Ok(new SuccessMessage(200, result));
        }


        //An endpoint to get all users who registered for an event
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsersRegisteredForAnEvent(Guid id)
        {
            var response = await _eventInterface.GetEventById(id);
            if (response == null)
            {
                return NotFound(new SuccessMessage(404, "Event Does Not Exist"));
            }
            var usersregistered =  await _eventInterface.GetAllUsersRegisteredForAnEvent(id);
            var users = _mapper.Map<IEnumerable<UserResponse>>(usersregistered);
            return Ok(users);
        }

        //An endpoint to show available slots for the event if the event is not full yet.
        [HttpGet("slots")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAvailableSlots(Guid id)
        {
            var response = await _eventInterface.GetEventById(id);
            if (response == null)
            {
                return NotFound(new SuccessMessage(404, "Event Does Not Exist"));
            }
            int slots = await _eventInterface.AvailableSlots(id);
            return Ok(slots);
        }

    }
}
