using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetCurrentCandidateCount();
    }
}