using System.ComponentModel.DataAnnotations;

namespace Assessment_5.Requests
{
    public class AddUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]

        public string Email { get; set; } = string.Empty;

        [Required]

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
