using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class NewsletterService
    {
        private readonly IMongoCollection<Newsletter> _newsletters;

        public NewsletterService(IMongoDatabase database)
        {
            _newsletters = database.GetCollection<Newsletter>("Newsletter");
        }

        public async Task<List<Newsletter>> GetAllAsync() =>
            await _newsletters.Find(_ => true).ToListAsync();

        public async Task<Newsletter> GetByIdAsync(string id) =>
            await _newsletters.Find(newsletter => newsletter.Id == id).FirstOrDefaultAsync();

        public async Task<Newsletter> GetByEmailAsync(string email) =>
            await _newsletters.Find(newsletter => newsletter.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(Newsletter newsletter)
        {
            newsletter.SubscribedAt = DateTime.UtcNow;
            newsletter.Subscribed = true;
            await _newsletters.InsertOneAsync(newsletter);
        }

        public async Task UpdateAsync(string id, Newsletter newsletter) =>
            await _newsletters.ReplaceOneAsync(n => n.Id == id, newsletter);

        public async Task DeleteAsync(string id) =>
            await _newsletters.DeleteOneAsync(n => n.Id == id);

        public async Task UnsubscribeAsync(string email)
        {
            var update = Builders<Newsletter>.Update
                .Set(n => n.Subscribed, false)
                .Set(n => n.UnsubscribedAt, DateTime.UtcNow);
            await _newsletters.UpdateOneAsync(n => n.Email == email, update);
        }
    }
}
