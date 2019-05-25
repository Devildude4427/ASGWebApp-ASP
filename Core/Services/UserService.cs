using System;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using Core.Persistence.Repositories;

namespace Core.Services
{
    public interface IUserService
    {
        Task<User> FindById(long id);
        Task<User> FindByEmail(string email);
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserIdentity _user;

        public UserService(IUserRepository userRepository, IUserIdentity user)
        {
            _userRepository = userRepository;
            _user = user;
        }
        
        public async Task<User> FindById(long id)
        {
            if (!_user.IsAuthorisedAtLevel(UserRole.Admin) && _user.Id != id)
                throw new UnauthorizedAccessException();
            
            return await _userRepository.FindById(id);
        }
        
        public async Task<User> FindByEmail(string email)
        {
            if (!_user.IsAuthorisedAtLevel(UserRole.Admin) && _user.Email != email)
                throw new UnauthorizedAccessException();
            
            return await _userRepository.FindByEmail(email);
        }
    }
}