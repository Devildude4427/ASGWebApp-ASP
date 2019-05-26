using System.Threading.Tasks;
using Autofac;
using Core.Services;
using FluentAssertions;
using Testing.Config;
using Xunit;

namespace Testing.Integration
{
    [Collection("Server")]
    public class StatisticsTests : Test
    {
        [Fact]
        public async Task CanGetCurrentCandidateCount()
        {
            using (var container = GetContainer())
            {
                var statisticsService = container.Resolve<IStatisticsService>();
                var count = await statisticsService.GetCurrentCandidateCount();
                count.Should().Be(2);
            }
        }
        
        [Fact]
        public async Task CanGetNewCandidateCount()
        {
            using (var container = GetContainer())
            {
                var statisticsService = container.Resolve<IStatisticsService>();
                var count = await statisticsService.GetNewCandidateCount();
                count.Should().Be(1);
            }
        }
    }
}