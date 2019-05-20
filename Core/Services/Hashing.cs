using BCrypt.Net;

namespace Core.Services
{
    public class Hashing
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12, SaltRevision.Revision2Y));
        }

        public static bool PasswordsMatch(string passwordToCheck, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, hash);
        }
    }
}