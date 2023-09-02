namespace Assessment_5.Responses
{
    public class SuccessMessage
    {
        public int StatusCode { get; set; } 
        public string Message { get; set; } 

        public SuccessMessage(int code, string message) { 
            this.StatusCode = code;
            this.Message = message;
        }
    }
}
