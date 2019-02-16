using System.Threading.Tasks;
using Domain.Entities;
using Domain.RepositoryInterfaces;

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
            
            if (user.Disabled)
                return new UserResponse(user, LoginResponse.UserDisabled);
            
            return new UserResponse(user, LoginResponse.Successful);
        }
    }
    
    public class UserResponse
    {
        public User User { get; }
        public LoginResponse LoginResponse { get; }

        public UserResponse(User user, LoginResponse loginResponse)
        {
            User = user;
            LoginResponse = loginResponse;
        }

        public UserResponse(LoginResponse loginResponse)
        {
            LoginResponse = loginResponse;
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
}