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
                var registrationRequest = new RegistrationRequest
                { Name = "Test User", Email = "test@asg.com", Password = "password12345" };
                var userResponse = await authService.Register(registrationRequest);
                userResponse.UserRegistrationResponse.Should().Be(UserRegistrationResponse.Successful);
            }
        }
    }
}