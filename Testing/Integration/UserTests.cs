using System.Threading.Tasks;
using Autofac;
using Core.Services;
using FluentAssertions;
using Testing.Config;
using Xunit;

namespace Testing.Integration
{
    [Collection("Server")]
    public class UserTests : Test
    {
        [Fact]
        public async Task CanFindById()
        {
            using (var container = GetContainer())
            {
                var userService = container.Resolve<IUserService>();
                var user = await userService.FindById(1);
                user.Name.Should().Be("Erwin Schroedinger");
                user.Role.Should().Be(9001);
            }
        }
        
        [Fact]
        public async Task CanFindByEmail()
        {
            using (var container = GetContainer())
            {
                var userService = container.Resolve<IUserService>();
                var user = await userService.FindByEmail("admin@asg.com");
                user.Name.Should().Be("Erwin Schroedinger");
                user.Role.Should().Be(9001);
            }
        }
    }
}