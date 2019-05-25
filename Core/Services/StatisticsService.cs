using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Core.Services
{
    public interface IStatisticsService
    {
        Task<int> GetCurrentCandidateCount();
        Task<int> GetNewCandidateCount();
    }
    
    class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        
        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }
        
        public async Task<int> GetCurrentCandidateCount()
        {
            return await _statisticsRepository.GetCurrentCandidateCount();
        }
        
        public async Task<int> GetNewCandidateCount()
        {
            return await _statisticsRepository.GetNewCandidateCount();
        }
    }
}