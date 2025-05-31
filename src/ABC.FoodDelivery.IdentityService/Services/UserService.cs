using ABC.FoodDelivery.IdentityService.DTOs;
using ABC.FoodDelivery.IdentityService.Entities;
using ABC.FoodDelivery.IdentityService.Helpers;
using ABC.FoodDelivery.IdentityService.Repositories;

namespace ABC.FoodDelivery.IdentityService.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserResponse> RegisterUserAsync(UserDto userDto)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            // Hash password
            var hashedPassword = _passwordHasher.HashPassword(userDto.Password);

            // Create user entity
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = hashedPassword,
                Role = userDto.Role,
                CreatedAt = DateTime.UtcNow
            };

            // Save to repository
            await _userRepository.AddUserAsync(user);

            return new UserResponse { UserId = user.UserId, Message = "User registered successfully." };
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
{
    var user = await _userRepository.GetUserByEmailAsync(email);
    if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
    {
        return null; // Invalid credentials
    }
    return user;
}
    }
}