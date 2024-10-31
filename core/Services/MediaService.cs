using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class MediaService
    {
        private readonly MediaRepository _repo;
        private readonly UserRepository _userRepo;
        private readonly IMapper _mapper;

        public MediaService(MediaRepository repo, UserRepository userRepo, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<Media> GetById(Guid id) => await _repo.FindByIdAsync(id) 
            ?? throw new NotFoundException("Media not found.");

        public async Task<Media> CreateMedia(MediaCreatePayload payload)
        {
            var user = await _userRepo.FindByIdAsync(payload.UserId)
                ?? throw new NotFoundException("User not found.");

            var ors = payload.Content.OpenReadStream();
            var ms = new MemoryStream();
            ors.CopyTo(ms);
            var bytes = ms.GetBuffer();

            var newMedia = new Media
            {
                User = user,
                Content = bytes
            };

            var savedMedia = await _repo.UpsertAsync(newMedia) 
                ?? throw new UpsertFailException("Media could not be created.");

            return savedMedia;
        }

        public async Task DeleteMedia(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);

            if (!exists)
                throw new NotFoundException("Media not found.");

            var deleted = await _repo.DeleteByIdAsync(id);

            if (!deleted)
                throw new DeleteException("Media couldn't be deleted.");
                
        }
    }
}
