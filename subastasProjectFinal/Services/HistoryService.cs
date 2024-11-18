using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class HistoryService
    {
        private readonly IMongoCollection<History> _history;

        public HistoryService(IMongoDatabase database)
        {
            _history = database.GetCollection<History>("History");
        }

        public async Task<List<History>> GetAllAsync() =>
            await _history.Find(_ => true).ToListAsync();

        public async Task<History> GetByIdAsync(string id) =>
            await _history.Find(history => history.Id == id).FirstOrDefaultAsync();

        public async Task<List<History>> GetByUserIdAsync(string userId) =>
            await _history.Find(history => history.UserId == userId)
                .SortByDescending(h => h.Timestamp)
                .ToListAsync();

        public async Task<List<History>> GetByActionTypeAsync(string action) =>
            await _history.Find(history => history.Action == action)
                .SortByDescending(h => h.Timestamp)
                .ToListAsync();

        public async Task CreateAsync(History history)
        {
            history.Timestamp = DateTime.UtcNow;
            await _history.InsertOneAsync(history);
        }

        public async Task DeleteAsync(string id) =>
            await _history.DeleteOneAsync(h => h.Id == id);

        public async Task DeleteOlderThanAsync(DateTime date) =>
            await _history.DeleteManyAsync(h => h.Timestamp < date);
    }
}
