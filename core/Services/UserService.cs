using AutoMapper;
using core.Data.Payloads;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace core.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;
        private readonly IMapper _mapper;

        private static readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(UserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(UserCreatePayload payload)
        {
            var newUser = new User();
            _mapper.Map(payload, newUser);

            newUser.Password = HashPassword(newUser, newUser.Password);

            var savedUser = await _repo.UpsertAsync(newUser)
                    ?? throw new Exception("User could not be inserted.");
            
            return savedUser;
        }

        public async Task<User> UpdateUser(Guid id, UserUpdatePayload payload)
        {
            var user = await _repo.FindByIdAsync(id)
                    ?? throw new Exception("User not found.");
                
            _mapper.Map(payload, user);

            var savedUser = await _repo.UpsertAsync(user)
                    ?? throw new Exception("User could not be updated.");
            
            return savedUser;
        }

        private static string HashPassword(User user, string raw)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, raw);
            return hashedPassword;
        }
    }
}