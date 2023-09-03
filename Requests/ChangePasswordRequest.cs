namespace Assessment_5.Requests
{
    public class ChangePasswordRequest

    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
    
}
