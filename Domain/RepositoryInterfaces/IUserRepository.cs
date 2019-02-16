using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        //CRUD
        
        Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest);
        Task<User> FindById(long id);
        Task<User> FindByEmail(string email);
        
        //Other
        Task<string> GetHashedPassword(string email);
    }
}