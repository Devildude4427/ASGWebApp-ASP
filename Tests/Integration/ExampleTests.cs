using System;
using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Services;
using Tests.Integration.Config;
using Xunit;

namespace Tests.Integration
{
    public class ExampleTests : IntegrationTest
    {
        // [Fact]
        // public async Task CanFindById()
        // {
        //     using (var container = GetContainer())
        //     {
        //         var service = container.Resolve<ExampleService>();
        //         var exampleEntity = await service.FindById(1);
        //         exampleEntity.Name.Should().Be("Test Example");
        //     }
        // }
        
        [Fact]
        public async Task CanGenerateReferenceNumber()
        {
            using (var container = GetContainer())
            {
                var service = container.Resolve<CandidateService>();
                var referenceNumber = await service.GenerateReferenceNumber();
                referenceNumber.Should().Be("ASG-001-" + DateTime.Now.ToString("yy-MM"));
            }
        }
    }
}