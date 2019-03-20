using System.Threading.Tasks;
using Dapper;
using Domain.RepositoryInterfaces;
using Persistence.Configuration;

namespace Persistence.Repositories
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
    }
}