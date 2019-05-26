using System.Threading.Tasks;
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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<User> FindById(long id)
        {
            
            return await _userRepository.FindById(id);
        }
        
        public async Task<User> FindByEmail(string email)
        {
            
            return await _userRepository.FindByEmail(email);
        }
    }
}