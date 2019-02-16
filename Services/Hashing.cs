namespace Services
{
    using BCrypt.Net;
    
    public class Hashing
    {
        public static string HashPassword(string password)
        {
            return BCrypt.HashPassword(password, BCrypt.GenerateSalt(12, SaltRevision.Revision2Y));
        }

        public static bool PasswordsMatch(string passwordToCheck, string hash)
        {
            return BCrypt.Verify(passwordToCheck, hash);
        }
    }
}