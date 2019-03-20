using System.Threading.Tasks;
using Domain.RepositoryInterfaces;

namespace Services
{
    public class StatisticsService
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
    }
}