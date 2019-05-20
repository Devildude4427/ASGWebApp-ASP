using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Testing.Config;
using Xunit;

namespace Testing.Integration
{
    public class CandidateTests : Test
    {
        // [Fact]
        // public async Task CanFindCandidateByUserId()
        // {
        //     using (var container = GetContainer())
        //     {
        //         var candidateService = container.Resolve<CandidateService>();
        //         var candidate = await candidateService.FindByUserId(1);
        //         candidate.ReferenceNumber.Should().Be("ASG-19-02-001");
        //     }
        // }
    }
}