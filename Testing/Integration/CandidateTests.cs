using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Core.Services;
using FluentAssertions;
using Testing.Config;
using Xunit;

namespace Testing.Integration
{
    [Collection("Server")]
    public class CandidateTests : Test
    {
        [Fact]
        public async Task CanGetAllCandidates()
        {
            using (var container = GetContainer())
            {
                var candidateService = container.Resolve<ICandidateService>();
                var candidateList = await candidateService.GetAll();
                candidateList.Count().Should().Be(2);
            }
        }
        
        [Fact]
        public async Task CanFindByUserId()
        {
            using (var container = GetContainer())
            {
                var candidateService = container.Resolve<ICandidateService>();
                var candidate = await candidateService.FindByUserId(1L);
                candidate.ReferenceNumber.Should().Be("ASG-19-02-001");
                candidate.LastCompletedStage.Should().Be(7);
            }
        }
    }
}