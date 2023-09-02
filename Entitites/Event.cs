namespace Assessment_5.Entitites
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; } =string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int Capacity { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;

        public int TicketPrice { get; set; } = 0;
        
        public List<User> Users { get; set; } = new List<User>();
    }
}
