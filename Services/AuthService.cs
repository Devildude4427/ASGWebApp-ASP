using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using Services.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Services
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
            
            CreateAuthToken(user);

            if (!user.Activated)
                return new UserResponse(user, LoginResponse.UserNotActivated);

            var hashedPassword = await _userRepository.GetHashedPassword(email);
            if (!Hashing.PasswordsMatch(password, hashedPassword))
                return new UserResponse(user, LoginResponse.IncorrectPassword);

            return !user.Enabled ? new UserResponse(user, LoginResponse.UserDisabled) :
                new UserResponse(user, LoginResponse.Successful);
        }
        
        private async Task<User> GetUserIfValid(string email) => await _userRepository.FindByEmail(email);

        private void CreateAuthToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
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
        public User User { get; }
        public LoginResponse LoginResponse { get; }
        public UserRegistrationResponse UserRegistrationResponse { get; }

        public UserResponse(User user, LoginResponse loginResponse)
        {
            User = user;
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