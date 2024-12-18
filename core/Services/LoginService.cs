using core.Data.Contexts;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace core.Services
{
    public class LoginService(
            UserRepository repo,
            IServiceProvider services)
    {
        private readonly UserRepository _repo = repo;
        private readonly IServiceProvider _services = services;
        private readonly PasswordHasher<User> _hasher = new();

        public async Task<LoginResult> TryLogin(LoginPayload payload)
        {
            var user = await _repo.FindByEmailAsync(payload.Email)
                    ?? throw new NotFoundException("User with this email not found.");

            var verification = _hasher.VerifyHashedPassword(
                user,
                user.Password!,
                payload.Password);
            
            bool emailMatches = user.Email!.Equals(payload.Email);
            bool passwordMatches = verification switch
            {
                PasswordVerificationResult.Success => true,
                _ => false,
            };

            if (!emailMatches || !passwordMatches)
                return new LoginResult.Failed();

            return new LoginResult.Succeeded()
            {
                UserId = user.Id,
                UserName = user.Name!,
                Subscription = user.Subscription!.Tier!.ModelNumber,
            };
        }

        public async Task<LoginResult> UpdateToken()
        {
            var userContext = _services.GetRequiredService<UserContext>();

            var user = await _repo.FindByIdEagerAsync(userContext.UserId);
            
            if (user is null)
                return new LoginResult.Failed();
            
            return new LoginResult.Succeeded()
            {
                UserId = user.Id,
                UserName = user.Name!,
                Subscription = user.Subscription!.Tier!.ModelNumber,
            };
        }
    }
}