using Assessment_5.Entitites;
using Assessment_5.Requests;
using Assessment_5.Responses;
using AutoMapper;

namespace Assessment_5.Profiles
{
    public class Eventsprofiles: Profile
    {
        public Eventsprofiles() 
        {
        CreateMap<AddUser, User>().ReverseMap();
        CreateMap<AddEvent, Event>().ReverseMap();
        CreateMap<UserResponse, User>().ReverseMap();
        CreateMap<EventResponse, Event>().ReverseMap();
        }
    }
}
