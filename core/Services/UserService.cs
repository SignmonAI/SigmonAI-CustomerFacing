using core.Data.Payloads;
using core.Models;
using Microsoft.AspNetCore.Identity;

namespace core.Services
{
    public class UserService
    {
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public User createUser(UserCreatePayload payload)
        {
            
        }

        private static string HashPassword(User user, string raw)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, raw);
            return hashedPassword;
        }
    }
}