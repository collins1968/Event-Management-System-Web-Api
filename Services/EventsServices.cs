using Assessment_5.Data;
using Assessment_5.Entitites;
using Assessment_5.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assessment_5.Services
{
    public class EventsServices : IEventsInterface
    {
        private readonly AppDbContext _context;

        public EventsServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateEvent(Event events)
        {
            _context.Events.Add(events);
            await _context.SaveChangesAsync();
            return "Event Added Successfully";
        }

        public async Task<string> DeleteEvent(Event events)
        {
             _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return "Event Deleted Successfully";
        }

        public async Task<List<Event>> GetAllEvents( string location)
        {
            if(string.IsNullOrEmpty(location))
            {
                return await _context.Events.ToListAsync();
            }
            var query = _context.Events.AsQueryable<Event>();
            if(!string.IsNullOrEmpty(location))
            {
                query = query.Where(x => x.Location == location);
            }

            return await query.ToListAsync();

        }

        public async Task<Event> GetEventById(Guid id)
        {
            var users = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            return users;
        }

        public async Task<string> UpdateEvent(Event events)
        {
            _context.Events.Update(events);
            await _context.SaveChangesAsync();
            return "Event Updated Successfully";

        }

        // getting all the users who have registered for an event
        public async Task<List<User>> GetAllUsersRegisteredForAnEvent(Guid id)
        {
            var result = await _context.Events.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
            return result.Users;
        }

        //get available slots
        public async Task<int> AvailableSlots(Guid id)
        {
            var result = await _context.Events
            .Include(e => e.Users)

                                .FirstOrDefaultAsync(e => e.Id == id);
            var availableSlots = result.Capacity - result.Users.Count;

            return availableSlots;
        }
    }
}
