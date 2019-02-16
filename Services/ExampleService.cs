using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Services
{
    public class ExampleService
    {
        private readonly IExampleRepository _exampleRepository;

        public ExampleService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public async Task<bool> Create(Example newEntity)
        {
            return await _exampleRepository.Create(newEntity);
        }
        
        public async Task<PaginatedList<Example>> Find(FilteredPageRequest pageRequest)
        {
            return await _exampleRepository.Find(pageRequest);
        }
        
        public async Task<Example> FindById(long id)
        {
            return await _exampleRepository.FindById(id);
        }
        
        public async Task<bool> Update(long id, Example updatedEntity)
        {
            return await _exampleRepository.Update(id, updatedEntity);
        }
        
        public async Task<bool> Delete(long id)
        {
            return await _exampleRepository.Delete(id);
        }
    }
}