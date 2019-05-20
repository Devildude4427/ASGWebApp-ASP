using System;
using Core.Domain;
using Core.Domain.Entities;

namespace Core.Services
{
    public class UserIdentity : IUserIdentity
    {
        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
        public UserRole Role { get; }
        public string AuthenticationType => "Session";
        public bool IsAuthenticated { get; }
        public static UserIdentity NoUser = new UserIdentity();

        private UserIdentity()
        {
            IsAuthenticated = false;
        }

        public UserIdentity(User user, bool isAuthenticated)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Role = user.Role;
            IsAuthenticated = isAuthenticated;
        }

        public UserIdentity(long id, string name, string email, int role, bool isAuthenticated)
        {
            Id = id;
            Name = name;
            Email = email;
            switch (role)
            {
                case 1001:
                    Role = UserRole.Standard;
                    break;
                case 9001:
                    Role = UserRole.Admin;
                    break;
                default:
                    throw new ArgumentException($"{role} is an invalid user role value.");
            }
            IsAuthenticated = isAuthenticated;
        }
    }
}