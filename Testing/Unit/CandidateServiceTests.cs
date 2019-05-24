using System;
using System.Threading.Tasks;
using Autofac;
using Core.Services;
using Core.Services.Course;
using FluentAssertions;
using Testing.Config;
using Xunit;

namespace Testing.Unit
{
    public class CandidateServiceTests : Test
    {
        [Fact]
        public async Task CanGenerateReferenceNumber()
        {
            using (var container = GetContainer())
            {
                var service = container.Resolve<CommercialService>();
                var referenceNumber = await service.GenerateReferenceNumber();
                referenceNumber.Should().Be("ASG-" + DateTime.Now.ToString("yy-MM") + "-001");
            }
        }
    }
}