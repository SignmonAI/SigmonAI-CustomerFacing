using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class BillService
    {
        private readonly BillRepository _repo;
        private readonly IMapper _mapper;

        public BillService(BillRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Bill> GetById(Guid id) => await _repo.FindByIdAsync(id) ?? throw new NotFoundException("Bill not found.");

        public async Task<IEnumerable<Bill>> GetBySubscriptionId(Guid subscriptionId)
        {
            var (bills, _) = await _repo.FindManyAsync();

            var subscriptionBills = bills.Where(b => b.Subscription!.Id == subscriptionId) ?? throw new NotFoundException("Subscription not found.");

            return subscriptionBills;
        }

        public async Task<Bill> CreateBill(BillCreatePayload payload)
        {
            var newBill = new Bill();
            _mapper.Map(payload, newBill);

            var savedBill = await _repo.UpsertAsync(newBill) ?? throw new UpsertFailException("Bill could not be inserted.");

            return savedBill;
        }

        public async Task<Bill> UpdateBill(Guid id, BillUpdatePayload payload)
        {
            var bill = await _repo.FindByIdAsync(id)
                     ?? throw new NotFoundException("Bill not found.");

            _mapper.Map(payload, bill);

            var savedBill = await _repo.UpsertAsync(bill)
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