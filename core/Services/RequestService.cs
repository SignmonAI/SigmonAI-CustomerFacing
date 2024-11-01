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
        private readonly UserRepository _userRepo;
        private readonly MediaRepository _mediaRepo;
        private readonly IMapper _mapper;

        public RequestService(RequestRepository repo, 
            UserRepository userRepo,
            MediaRepository mediaRepo, 
            IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mediaRepo = mediaRepo;
            _mapper = mapper;
        }

        public async Task<Request> CreateRequest(RequestCreatePayload payload)
        {
            var newRequest = new Request();
            _mapper.Map(payload, newRequest);

            var user = await _userRepo.FindByIdAsync(payload.UserId) 
                ?? throw new NotFoundException("User not found.");
            newRequest.User = user;
            user.Requests!.Add(newRequest);

            var media = await _mediaRepo.FindByIdAsync(payload.MediaId) 
                ?? throw new NotFoundException("Media not found.");
            newRequest.Media = media;

            var currentDate = DateTime.Now;
            newRequest.Date = DateOnly.FromDateTime(currentDate);

            var request = await _repo.UpsertAsync(newRequest)
                    ?? throw new UpsertFailException("Request could not be created.");
            
            return request;
        }

        public async Task<Request> GetById(Guid id)
        {
            var request = await _repo.EagerFindByIdAsync(id)
                    ?? throw new NotFoundException("Request not found.");
            
            return request;
        }

        public async Task<IEnumerable<Request>> GetByUserId(Guid userId)
        {
            var requests = await _repo.EagerFindManyAsync();
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