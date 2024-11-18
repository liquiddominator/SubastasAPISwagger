using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class AuctionService
    {
        private readonly IMongoCollection<Auction> _auctions;

        public AuctionService(IMongoDatabase database)
        {
            _auctions = database.GetCollection<Auction>("Auctions");
        }

        public async Task<List<Auction>> GetAllAsync() =>
            await _auctions.Find(_ => true).ToListAsync();

        public async Task<Auction> GetByIdAsync(string id) =>
            await _auctions.Find(auction => auction.Id == id).FirstOrDefaultAsync();

        public async Task<List<Auction>> GetBySellerIdAsync(string sellerId) =>
            await _auctions.Find(auction => auction.SellerId == sellerId).ToListAsync();

        public async Task<List<Auction>> GetByCategoryAsync(string category) =>
            await _auctions.Find(auction => auction.Category == category).ToListAsync();

        public async Task<List<Auction>> GetActiveAuctionsAsync() =>
            await _auctions.Find(auction =>
                auction.Status == "active" &&
                auction.EndDate > DateTime.UtcNow)
                .ToListAsync();

        public async Task CreateAsync(Auction auction)
        {
            auction.Status = "active";
            auction.StartDate = DateTime.UtcNow;
            auction.CurrentPrice = auction.StartingPrice;
            await _auctions.InsertOneAsync(auction);
        }

        public async Task UpdateAsync(string id, Auction auction) =>
            await _auctions.ReplaceOneAsync(a => a.Id == id, auction);

        public async Task DeleteAsync(string id) =>
            await _auctions.DeleteOneAsync(a => a.Id == id);

        public async Task UpdateCurrentPriceAsync(string id, decimal newPrice) =>
            await _auctions.UpdateOneAsync(
                a => a.Id == id,
                Builders<Auction>.Update.Set(a => a.CurrentPrice, newPrice));

        public async Task UpdateStatusAsync(string id, string status) =>
            await _auctions.UpdateOneAsync(
                a => a.Id == id,
                Builders<Auction>.Update.Set(a => a.Status, status));
    }
}
