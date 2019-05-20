using System.Threading.Tasks;
using Core.Domain.RepositoryInterfaces;
using Core.Persistence.Configuration;
using Dapper;

namespace Core.Persistence.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DatabaseConnection _con;

        public StatisticsRepository(DatabaseConnection con)
        {
            _con = con;
        }
        
        public async Task<int> GetCurrentCandidateCount()
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM  candidates;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<int>(sql);
        }
        
        public async Task<int> GetNewCandidateCount()
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM candidates
                WHERE registration_date > current_date - interval '7 days';
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<int>(sql);
        }
    }
}