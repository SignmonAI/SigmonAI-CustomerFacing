using core.Data.Outbound;
using core.Data.Payloads;
using core.Errors;
using core.Repositories;

namespace core.Services
{
    public class LoginService
    {
        private UserRepository _repo

        public LoginService(UserRepository repo)
        {
            _repo = repo;
        }

        public Task<LoginResult> TryLogin(LoginPayload payload)
        {
            var user = _repo.FindByEmailAsync(payload.Email)
                    ?? throw new AuthenticationException("")
        }
    }
}