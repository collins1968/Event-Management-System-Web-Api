namespace Assessment_5.Requests
{
    public class ChangePasswordRequest

    {
        public string Email { get; set; }
        public string NewPassword { get; set; } 
        public string ConfirmPassword { get; set; }
    }
    
}
