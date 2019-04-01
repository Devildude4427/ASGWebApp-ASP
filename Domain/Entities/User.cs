namespace Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public bool Activated { get; set; }
        public bool Enabled { get; set; }
        public string Token { get; set; }
    }

    public class NewUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role = UserRole.Standard;
        public string PasswordHash { get; set; }
    }
}