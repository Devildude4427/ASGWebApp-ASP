using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Core.Domain.ViewModels;
using Core.Services.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        
        private readonly AppSettings _appSettings;

        public AuthService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UserResponse> Login(string email, string password)
        {
            var user = await GetUserIfValid(email);

            if (user == null)
                return new UserResponse(LoginResponse.UserNonExistent);
            
            var jwtToken = CreateAuthToken(user);

            if (!user.Activated)
                return new UserResponse(LoginResponse.UserNotActivated);

            var hashedPassword = await _userRepository.GetHashedPassword(email);
            if (!Hashing.PasswordsMatch(password, hashedPassword))
                return new UserResponse(LoginResponse.IncorrectPassword);

            return !user.Enabled ? new UserResponse(LoginResponse.UserDisabled) :
                new UserResponse(jwtToken, LoginResponse.Successful);
        }
        
        private async Task<User> GetUserIfValid(string email) => await _userRepository.FindByEmail(email);

        private string CreateAuthToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(0.5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UserResponse> Register(RegistrationRequest registrationRequest) =>
            await RegisterUserIfValid(registrationRequest);
        
        private async Task<UserResponse> RegisterUserIfValid(RegistrationRequest registrationRequest)
        {
            var user = await _userRepository.FindByEmail(registrationRequest.Email);
            if (user != null)
                return new UserResponse(UserRegistrationResponse.UserAlreadyExists);

            registrationRequest.Password = Hashing.HashPassword(registrationRequest.Password);
            
            var registered = await _userRepository.Register(registrationRequest);
            return registered ? new UserResponse(UserRegistrationResponse.Successful)
                : new UserResponse(UserRegistrationResponse.UnknownError);
        }
    }
    
    public class UserResponse
    {
        public string JwtToken { get; }
        public LoginResponse LoginResponse { get; }
        public UserRegistrationResponse UserRegistrationResponse { get; }

        public UserResponse(string jwtToken, LoginResponse loginResponse)
        {
            JwtToken = jwtToken;
            LoginResponse = loginResponse;
        }

        public UserResponse(LoginResponse loginResponse)
        {
            LoginResponse = loginResponse;
        }
        
        public UserResponse(UserRegistrationResponse userRegistrationResponse)
        {
            UserRegistrationResponse = userRegistrationResponse;
        }
    }

    public enum LoginResponse
    {
        AlreadyLoggedIn,
        Successful,
        UserNotActivated,
        UserNonExistent,
        IncorrectPassword,
        UserDisabled,
        UnknownError
    }
    
    public enum UserRegistrationResponse
    {
        UserAlreadyExists,
        Successful,
        UnknownError
    }
}