using AutoMapper;
using core.Data.Payloads;
using core.Data.Queries;
using core.Errors;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace core.Services
{
    public class UserService(UserRepository repo, IMapper mapper)
    {
        private readonly UserRepository _repo = repo;
        private readonly IMapper _mapper = mapper;

        private static readonly PasswordHasher<User> _passwordHasher = new();

        public async Task<User> CreateUser(UserCreatePayload payload)
        {
            var newUser = new User();
            _mapper.Map(payload, newUser);

            newUser.Password = HashPassword(newUser, newUser.Password!);

            var savedUser = await _repo.UpsertAsync(newUser)
                    ?? throw new UpsertFailException("User could not be inserted.");
            
            return savedUser;
        }

        public async Task<User> UpdateUser(Guid id, UserUpdatePayload payload)
        {
            var user = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("User not found.");
                
            _mapper.Map(payload, user);

            var savedUser = await _repo.UpsertAsync(user)
                    ?? throw new UpsertFailException("User could not be updated.");
            
            return savedUser;
        }

        public async Task<User> FetchUser(Guid id)
        {
            var user = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("User not found.");
            
            return user;
        }

        public async Task<(IEnumerable<User>, PaginationInfo?)> FetchManyUsers(PaginationQuery pagination)
        {
            var options = pagination.ToOptions();
            var paginatedData = await _repo.FindManyAsync(options);
            
            if (!paginatedData.Item1.Any())
                throw new NotFoundException("Couldn't find matching data.");
            
            return paginatedData;
        }

        private static string HashPassword(User user, string raw)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, raw);
            return hashedPassword;
        }
    }
}