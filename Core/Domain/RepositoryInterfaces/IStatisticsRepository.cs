using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetCurrentCandidateCount();
        Task<int> GetNewCandidateCount();
    }
}