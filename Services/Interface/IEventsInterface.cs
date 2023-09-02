using Assessment_5.Entitites;

namespace Assessment_5.Services.Interface
{
    public interface IEventsInterface
    {
        //add event
        Task<string> CreateEvent(Event events);

        //update event
        Task<string> UpdateEvent(Event events);

        //delete event
        Task<string> DeleteEvent(Event events);

        //get all events
        Task<List<Event>> GetAllEvents(string location);

        //get event by id
        Task<Event> GetEventById(Guid id);

        //get all users who registered for an event
        Task<List<User>> GetAllUsersRegisteredForAnEvent(Guid id);
        //get available slots
        Task<int> AvailableSlots(Guid id);
    }
}
