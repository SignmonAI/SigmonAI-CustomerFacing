using System.Text.RegularExpressions;
using AutoMapper;
using core.Data.Payloads;
using core.Data.Queries;
using core.Errors;
using core.Models;
using core.Repositories;
using core.Services.Factories;
using Microsoft.AspNetCore.Identity;

namespace core.Services
{
    public class UserService(
            UserRepository repo,
            CountryRepository countryRepo,
            ITierFactory tierFactory,
            IMapper mapper)
    {
        private readonly UserRepository _repo = repo;
        private readonly CountryRepository _countryRepo = countryRepo;
        private readonly ITierFactory _tierFactory = tierFactory;
        private readonly IMapper _mapper = mapper;

        private static readonly PasswordHasher<User> _passwordHasher = new();

        public async Task<User> CreateUser(UserCreatePayload payload)
        {
            var exists = await _repo.FindByEmailAsync(payload.Email);

            if (exists is not null)
                throw new AlreadyExistsException("Email already in use.");

            var newUser = new User();
            _mapper.Map(payload, newUser);

            var treatedPhone = Regex.Replace(payload.Phone, @"\D", "");
            var country = await _countryRepo.FindByIdAsync(payload.CountryId);

            newUser.Country = country;
            newUser.Phone = treatedPhone;
            newUser.Password = HashPassword(newUser, newUser.Password!);

            newUser.Subscription = new Subscription
            {
                Tier = await _tierFactory.CreateTier(ClassificationModel.FREE)
            };

            var savedUser = await _repo.UpsertAsync(newUser)
                    ?? throw new UpsertFailException("User could not be inserted.");
            
            return savedUser;
        }

        public async Task<User> UpdateUser(Guid id, UserUpdatePayload payload)
        {
            var user =
                await _repo.FindByIdAsync(id) ?? throw new NotFoundException("User not found.");

            if (payload.Email is not null)
            {
                var exists = await _repo.FindByEmailAsync(payload.Email);

                if (exists is not null)
                    throw new AlreadyExistsException("Email already in use.");
            }

            _mapper.Map(payload, user);

            if (payload.Phone is not null)
                user.Phone = Regex.Replace(payload.Phone, @"\D", "");;

            if (payload.Password is not null)
                user.Password = HashPassword(user, user.Password!);

            if (payload.CountryId.HasValue)
                user.Country = await _countryRepo.FindByIdAsync(payload.CountryId.Value);

            var savedUser =
                await _repo.UpsertAsync(user)
                ?? throw new UpsertFailException("User could not be updated.");

            return savedUser;
        }

        public async Task<User> FetchUser(Guid id)
        {
            var user =
                await _repo.FindByIdAsync(id) ?? throw new NotFoundException("User not found.");

            return user;
        }

        public async Task<(IEnumerable<User>, PaginationInfo?)> FetchManyUsers(
            PaginationQuery pagination
        )
        {
            var options = pagination.ToOptions();
            var paginatedData = await _repo.FindManyAsync(options);

            if (!paginatedData.Item1.Any())
                throw new NotFoundException("Couldn't find matching data.");

            return paginatedData;
        }

        public async Task<bool> UpdateSubscriptionTier(
                Guid id,
                UserChangeTierPayload payload)
        {
            var user = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("User not found.");
            
            user.Subscription!.Tier = await _tierFactory.CreateTier(payload.NewTier);

            var savedUser = await _repo.UpsertAsync(user);

            return savedUser is not null;
        }

        private static string HashPassword(User user, string raw)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, raw);
            return hashedPassword;
        }
    }
}
