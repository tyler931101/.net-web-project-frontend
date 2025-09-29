namespace frontend.Models
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
        public string? UserId { get; set; }
    }
}