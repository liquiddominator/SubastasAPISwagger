using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryService(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _categories.Find(_ => true).ToListAsync();

        public async Task<Category> GetByIdAsync(string id) =>
            await _categories.Find(category => category.Id == id).FirstOrDefaultAsync();

        public async Task<List<Category>> GetActiveAsync() =>
            await _categories.Find(category => category.Active).ToListAsync();

        public async Task<List<Category>> GetByParentIdAsync(string parentId) =>
            await _categories.Find(category => category.ParentId == parentId).ToListAsync();

        public async Task CreateAsync(Category category)
        {
            category.Active = true;
            await _categories.InsertOneAsync(category);
        }

        public async Task UpdateAsync(string id, Category category) =>
            await _categories.ReplaceOneAsync(c => c.Id == id, category);

        public async Task DeleteAsync(string id) =>
            await _categories.DeleteOneAsync(c => c.Id == id);

        public async Task ToggleActiveStatusAsync(string id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                await _categories.UpdateOneAsync(
                    c => c.Id == id,
                    Builders<Category>.Update.Set(c => c.Active, !category.Active));
            }
        }
    }
}
