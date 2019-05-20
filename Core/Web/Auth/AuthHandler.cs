using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Core.Web.Auth
{
    public class AuthHandler : AuthenticationHandler<AuthOptions>
    {
        public const string AuthName = "ASGAuth";
        
        public AuthHandler(IOptionsMonitor<AuthOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var user = SessionManager.GetUserFromSession(Request);
            if (user == null)
                return Task.FromResult(AuthenticateResult.NoResult());

            Context.Items["User"] = user;
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, ((int) user.Role).ToString())
            }, AuthName);
            
            var principle = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(principle, AuthName);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
   
    public class AuthOptions : AuthenticationSchemeOptions
    {
        public static string AuthenticationScheme => "ASGAuth";
    }

    public static class AuthExtensions
    {
        public static AuthenticationBuilder AddAuth(this AuthenticationBuilder builder, Action<AuthOptions> configureOptions)
        {
            return builder.AddScheme<AuthOptions, AuthHandler>(AuthHandler.AuthName, "ASG Auth", configureOptions);
        }
    }
    
    public static class SessionManager
    {
        public static bool Secure { get; set; }
    
        public static HttpResponse WithCredentials(this HttpResponse response, User user)
        {
            if (user == null) return response;
            var session = response.HttpContext.Session;
            session.Set("userId", user.Id);
            session.SetString("name", user.Name);
            session.SetString("email", user.Email);
            session.SetInt32("role", (int)user.Role);
            return response;
        }
    
        public static HttpResponse DeleteCredentials(this HttpResponse response)
        {
            var session = response.HttpContext.Session;
            session.Clear();
            return response;
        }
    
        public static UserIdentity GetUserFromSession(HttpRequest request)
        {
            var session = request.HttpContext.Session;
    
            if (!session.IsAvailable) return null;
            var id = session.Get<long?>("userId");
            var name = session.GetString("name");
            var email = session.GetString("email");
            var role = session.GetInt32("role");
            if (id == null || role == null || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                role == 0) return null;
            
            return new UserIdentity(id.Value, name, email, role.Value, true);
        }
    }
    
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
    
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}