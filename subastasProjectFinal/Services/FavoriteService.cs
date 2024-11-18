using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class FavoriteService
    {
        private readonly IMongoCollection<Favorite> _favorites;

        public FavoriteService(IMongoDatabase database)
        {
            _favorites = database.GetCollection<Favorite>("Favorites");
        }

        public async Task<List<Favorite>> GetAllAsync() =>
            await _favorites.Find(_ => true).ToListAsync();

        public async Task<Favorite> GetByIdAsync(string id) =>
            await _favorites.Find(favorite => favorite.Id == id).FirstOrDefaultAsync();

        public async Task<List<Favorite>> GetByUserIdAsync(string userId) =>
            await _favorites.Find(favorite => favorite.UserId == userId).ToListAsync();

        public async Task<List<Favorite>> GetByAuctionIdAsync(string auctionId) =>
            await _favorites.Find(favorite => favorite.AuctionId == auctionId).ToListAsync();

        public async Task<bool> IsFavoriteAsync(string userId, string auctionId) =>
            await _favorites.Find(f => f.UserId == userId && f.AuctionId == auctionId)
                .AnyAsync();

        public async Task CreateAsync(Favorite favorite)
        {
            favorite.AddedAt = DateTime.UtcNow;
            await _favorites.InsertOneAsync(favorite);
        }

        public async Task DeleteAsync(string id) =>
            await _favorites.DeleteOneAsync(f => f.Id == id);

        public async Task DeleteByUserAndAuctionAsync(string userId, string auctionId) =>
            await _favorites.DeleteOneAsync(f => f.UserId == userId && f.AuctionId == auctionId);
    }
}
