namespace Core.Domain.Models.Authentication
{
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}