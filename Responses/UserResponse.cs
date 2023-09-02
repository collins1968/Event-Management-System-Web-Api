using Assessment_5.Entitites;

namespace Assessment_5.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        //public List<Event> Events { get; set; } = new List<Event>();
    }
}
