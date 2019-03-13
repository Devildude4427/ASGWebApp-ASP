using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ViewModels;

namespace Services
{
    public class AccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Login(string email, string password) => await GetUserIfValid(email, password);

        private async Task<UserResponse> GetUserIfValid(string email, string password)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user == null)
                return new UserResponse(LoginResponse.UserNonExistent);
            
            if (!user.Activated)
                return new UserResponse(user, LoginResponse.UserNotActivated);

            var hashedPassword = await _userRepository.GetHashedPassword(email);
            if (!Hashing.PasswordsMatch(password, hashedPassword))
                return new UserResponse(user, LoginResponse.IncorrectPassword);
            
            return !user.Enabled ? new UserResponse(user, LoginResponse.UserDisabled) :
                new UserResponse(user, LoginResponse.Successful);
        }

        public async Task<UserResponse> Register(RegistrationRequest registrationRequest) =>
            await RegisterUserIfValid(registrationRequest);
        
        private async Task<UserResponse> RegisterUserIfValid(RegistrationRequest registrationRequest)
        {
            var user = await _userRepository.FindByEmail(registrationRequest.Email);
            if (user != null)
                return new UserResponse(RegistrationResponse.UserAlreadyExists);

            registrationRequest.Password = Hashing.HashPassword(registrationRequest.Password);
            
            var returnVariable = await _userRepository.Register(registrationRequest);
            return returnVariable ? new UserResponse(RegistrationResponse.Successful)
                : new UserResponse(RegistrationResponse.UnknownError);
        }
    }
    
    public class UserResponse
    {
        public User User { get; }
        public LoginResponse LoginResponse { get; }
        public RegistrationResponse RegistrationResponse { get; }

        public UserResponse(User user, LoginResponse loginResponse)
        {
            User = user;
            LoginResponse = loginResponse;
        }

        public UserResponse(LoginResponse loginResponse)
        {
            LoginResponse = loginResponse;
        }
        
        public UserResponse(RegistrationResponse registrationResponse)
        {
            RegistrationResponse = registrationResponse;
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
    
    public enum RegistrationResponse
    {
        UserAlreadyExists,
        Successful,
        UnknownError
    }
}