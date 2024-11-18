using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentService(IMongoDatabase database)
        {
            _comments = database.GetCollection<Comment>("Comments");
        }

        public async Task<List<Comment>> GetAllAsync() =>
            await _comments.Find(_ => true).ToListAsync();

        public async Task<Comment> GetByIdAsync(string id) =>
            await _comments.Find(comment => comment.Id == id).FirstOrDefaultAsync();

        public async Task<List<Comment>> GetByAuctionIdAsync(string auctionId) =>
            await _comments.Find(comment => comment.AuctionId == auctionId)
                .SortByDescending(c => c.Timestamp)
                .ToListAsync();

        public async Task<List<Comment>> GetByUserIdAsync(string userId) =>
            await _comments.Find(comment => comment.UserId == userId)
                .SortByDescending(c => c.Timestamp)
                .ToListAsync();

        public async Task CreateAsync(Comment comment)
        {
            comment.Timestamp = DateTime.UtcNow;
            comment.Likes = 0;
            comment.Status = "active";
            await _comments.InsertOneAsync(comment);
        }

        public async Task UpdateAsync(string id, Comment comment) =>
            await _comments.ReplaceOneAsync(c => c.Id == id, comment);

        public async Task DeleteAsync(string id) =>
            await _comments.DeleteOneAsync(c => c.Id == id);

        public async Task IncrementLikesAsync(string id) =>
            await _comments.UpdateOneAsync(
                c => c.Id == id,
                Builders<Comment>.Update.Inc(c => c.Likes, 1));
    }
}
