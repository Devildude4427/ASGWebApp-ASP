using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IExampleRepository
    {
        Task<bool> Create(Example newEntity);
        Task<PaginatedList<Example>> Find(FilteredPageRequest pageRequest);
        Task<Example> FindById(long id);
        Task<bool> Update(long id, Example updatedEntity);
        Task<bool> Delete(long id);
    }
}