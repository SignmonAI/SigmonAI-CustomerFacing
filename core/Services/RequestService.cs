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
    public class RequestService
    {
        private readonly RequestRepository _repo;
        private readonly IMapper _mapper;

        private static readonly PasswordHasher<User> _passwordHasher = new();

        public RequestService(RequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Request> CreateRequest(RequestCreatePayload payload)
        {
            var newRequest = new Request();
            _mapper.Map(payload, newRequest);

            var request = await _repo.UpsertAsync(newRequest)
                    ?? throw new UpsertFailException("Request could not be created.");
            
            return request;
        }

        public async Task<Request> GetById(Guid id)
        {
            var request = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("Request not found.");
            
            return request;
        }

        public async Task<IEnumerable<Request>> GetByUserId(Guid userId)
        {
            var (requests, _) = await _repo.FindManyAsync();
            var usersRequests = requests.Where(req => req.User!.Id == userId);
            
            return usersRequests;
        }

        public async Task DeleteRequest(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);

            if (!exists)
                throw new NotFoundException("Subscription not found.");

            var deleted = await _repo.DeleteByIdAsync(id);

            if (!deleted)
                throw new DeleteException("Subscription couldn't be deleted.");
        }
    }
}