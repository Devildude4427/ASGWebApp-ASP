using System.Security.Principal;

namespace Domain
{
    public interface IUserIdentity : IIdentity
    {
        long Id { get; }
        string Email { get; }
        UserRole Role { get; }
    }

    public enum UserRole
    {
        Standard = 1001,
        Admin = 9001
    }
    
    public static class AuthorisationExtensions
    {
        public static bool IsAuthorisedAtLevel(this IUserIdentity user, UserRole requiredRole)
        {
            return (int) user.Role >= (int) requiredRole;
        }
    }
}