using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class BillService(
        BillRepository repo,
        IMapper mapper,
        SubscriptionRepository subscriptionRepo
    )
    {
        private readonly BillRepository _repo = repo;
        private readonly SubscriptionRepository _subscriptionRepo = subscriptionRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<Bill> GetById(Guid id) =>
            await _repo.FindByIdAsync(id) ?? throw new NotFoundException("Bill not found.");

        public async Task<IEnumerable<Bill>> GetBySubscriptionId(Guid subscriptionId) =>
            await _repo.FindBySubscriptionId(subscriptionId);

        public async Task<Bill> CreateBill(BillCreatePayload payload)
        {
            var newBill = new Bill();

            _mapper.Map(payload, newBill);

            var subscription = await _subscriptionRepo.FindByIdAsync(payload.SubscriptionId);
            newBill.Subscription = subscription;

            var savedBill =
                await _repo.UpsertAsync(newBill)
                ?? throw new UpsertFailException("Bill could not be inserted.");

            return savedBill;
        }

        public async Task<Bill> UpdateBill(Guid id, BillUpdatePayload payload)
        {
            var bill =
                await _repo.FindByIdAsync(id) ?? throw new NotFoundException("Bill not found.");

            _mapper.Map(payload, bill);

            if (payload.SubscriptionId.HasValue)
            {
                var subscription = await _subscriptionRepo.FindByIdAsync(payload.SubscriptionId.Value);
                bill.Subscription = subscription;
            }

            var savedBill =
                await _repo.UpsertAsync(bill)
                ?? throw new UpsertFailException("Bill could not be updated.");

            return savedBill;
        }

        public async Task DeleteBill(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);

            if (!exists)
                throw new NotFoundException("Bill not found.");

            var deleted = await _repo.DeleteByIdAsync(id);

            if (!deleted)
                throw new DeleteException("Bill couldn't be deleted.");
        }
    }
}
