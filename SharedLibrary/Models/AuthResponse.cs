namespace SharedLibrary.Models
{
    public class AuthResponse
    {
        public bool IsSuccessful { get; set; }
        public string? Error { get; set; }

        public User? User { get; set; }
    }
}
