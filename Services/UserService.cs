using System;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserIdentity _user;

        public UserService(IUserRepository userRepository, IUserIdentity user)
        {
            _userRepository = userRepository;
            _user = user;
        }


        public async Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest)
        {
            if (!_user.IsAuthorisedAtLevel(UserRole.Admin))
                throw new UnauthorizedAccessException();
            
            return await _userRepository.Find(filteredPageRequest);
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