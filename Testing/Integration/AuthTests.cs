using System.Threading.Tasks;
using Autofac;
using Core.Domain.Models.Authentication;
using Core.Services;
using FluentAssertions;
using Testing.Config;
using Xunit;

namespace Testing.Integration
{
    [Collection("Server")]
    public class AuthTests : Test
    {
        [Fact]
        public async Task CanRegisterNewUser()
        {
            using (var container = GetContainer())
            {
                var authService = container.Resolve<IAuthService>();
                var userResponse = await authService.Register(
                    new RegistrationRequest("Test User", "testuser@asg.com", "password12345"));
                userResponse.UserRegistrationResponse.Should().Be(UserRegistrationResponse.Successful);
            }
        }
    }
}