using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class FAQService
    {
        private readonly IMongoCollection<FAQ> _faqs;

        public FAQService(IMongoDatabase database)
        {
            _faqs = database.GetCollection<FAQ>("Faq");
        }

        public async Task<List<FAQ>> GetAllAsync() =>
            await _faqs.Find(_ => true).ToListAsync();

        public async Task<FAQ> GetByIdAsync(string id) =>
            await _faqs.Find(faq => faq.Id == id).FirstOrDefaultAsync();

        public async Task<List<FAQ>> GetActiveFAQsAsync() =>
            await _faqs.Find(faq => faq.Active)
                .SortBy(f => f.Order)
                .ToListAsync();

        public async Task<List<FAQ>> GetByCategoryAsync(string category) =>
            await _faqs.Find(faq => faq.Category == category)
                .SortBy(f => f.Order)
                .ToListAsync();

        public async Task CreateAsync(FAQ faq)
        {
            faq.Active = true;
            await _faqs.InsertOneAsync(faq);
        }

        public async Task UpdateAsync(string id, FAQ faq) =>
            await _faqs.ReplaceOneAsync(f => f.Id == id, faq);

        public async Task DeleteAsync(string id) =>
            await _faqs.DeleteOneAsync(f => f.Id == id);

        public async Task ToggleActiveStatusAsync(string id)
        {
            var faq = await GetByIdAsync(id);
            if (faq != null)
            {
                await _faqs.UpdateOneAsync(
                    f => f.Id == id,
                    Builders<FAQ>.Update.Set(f => f.Active, !faq.Active));
            }
        }
    }
}
