using System.ComponentModel.DataAnnotations;

namespace Assessment_5.Requests
{
    public class AddEvent
    {   
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int TicketPrice { get; set; } = 0;
    }
}
