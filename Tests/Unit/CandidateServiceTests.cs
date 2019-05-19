using System;
using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Services;
using Tests.Unit.Config;
using Xunit;

namespace Tests.Unit
{
    public class CandidateServiceTests : UnitTest
    {
        [Fact]
        public async Task CanGenerateReferenceNumber()
        {
            using (var container = GetContainer())
            {
                var service = container.Resolve<CandidateService>();
                var referenceNumber = await service.GenerateReferenceNumber();
                referenceNumber.Should().Be("ASG-" + DateTime.Now.ToString("yy-MM") + "-001");
            }
        }
    }
}