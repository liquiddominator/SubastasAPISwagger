using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class BidService
    {
        private readonly IMongoCollection<Bid> _bids;

        public BidService(IMongoDatabase database)
        {
            _bids = database.GetCollection<Bid>("Bids");
        }

        public async Task<List<Bid>> GetAllAsync() =>
            await _bids.Find(_ => true).ToListAsync();

        public async Task<Bid> GetByIdAsync(string id) =>
            await _bids.Find(bid => bid.Id == id).FirstOrDefaultAsync();

        public async Task<List<Bid>> GetByAuctionIdAsync(string auctionId) =>
            await _bids.Find(bid => bid.AuctionId == auctionId)
                .SortByDescending(b => b.Amount)
                .ToListAsync();

        public async Task<List<Bid>> GetByBidderIdAsync(string bidderId) =>
            await _bids.Find(bid => bid.BidderId == bidderId).ToListAsync();

        public async Task<Bid> GetHighestBidForAuctionAsync(string auctionId) =>
            await _bids.Find(bid => bid.AuctionId == auctionId)
                .SortByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

        public async Task CreateAsync(Bid bid)
        {
            bid.Timestamp = DateTime.UtcNow;
            bid.Status = "active";
            await _bids.InsertOneAsync(bid);
        }

        public async Task UpdateAsync(string id, Bid bid) =>
            await _bids.ReplaceOneAsync(b => b.Id == id, bid);

        public async Task DeleteAsync(string id) =>
            await _bids.DeleteOneAsync(b => b.Id == id);
    }
}
