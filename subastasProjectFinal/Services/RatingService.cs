using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class RatingService
    {
        private readonly IMongoCollection<Rating> _ratings;

        public RatingService(IMongoDatabase database)
        {
            _ratings = database.GetCollection<Rating>("Ratings");
        }

        public async Task<List<Rating>> GetAllAsync() =>
            await _ratings.Find(_ => true).ToListAsync();

        public async Task<Rating> GetByIdAsync(string id) =>
            await _ratings.Find(rating => rating.Id == id).FirstOrDefaultAsync();

        public async Task<List<Rating>> GetByFromUserIdAsync(string fromUserId) =>
            await _ratings.Find(rating => rating.FromUserId == fromUserId).ToListAsync();

        public async Task<List<Rating>> GetByToUserIdAsync(string toUserId) =>
            await _ratings.Find(rating => rating.ToUserId == toUserId).ToListAsync();

        public async Task CreateAsync(Rating rating)
        {
            rating.Timestamp = DateTime.UtcNow;
            await _ratings.InsertOneAsync(rating);
        }

        public async Task UpdateAsync(string id, Rating rating) =>
            await _ratings.ReplaceOneAsync(r => r.Id == id, rating);

        public async Task DeleteAsync(string id) =>
            await _ratings.DeleteOneAsync(r => r.Id == id);

        public async Task<double> GetAverageRatingForUserAsync(string userId)
        {
            var ratings = await _ratings.Find(r => r.ToUserId == userId).ToListAsync();
            return ratings.Any() ? ratings.Average(r => r.RatingValue) : 0;
        }
    }
}
