using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.ViewModels;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        //CRUD
        
        Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest);
        Task<User> FindById(long id);
        Task<User> FindByEmail(string email);
        Task<bool> Register(RegistrationRequest registrationRequest);
        
        //Other
        Task<string> GetHashedPassword(string email);
    }
}