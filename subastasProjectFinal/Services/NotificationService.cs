using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class NotificationService
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationService(IMongoDatabase database)
        {
            _notifications = database.GetCollection<Notification>("Notifications");
        }

        public async Task<List<Notification>> GetAllAsync() =>
            await _notifications.Find(_ => true).ToListAsync();

        public async Task<Notification> GetByIdAsync(string id) =>
            await _notifications.Find(notification => notification.Id == id).FirstOrDefaultAsync();

        public async Task<List<Notification>> GetByUserIdAsync(string userId) =>
            await _notifications.Find(notification => notification.UserId == userId).ToListAsync();

        public async Task<List<Notification>> GetUnreadByUserIdAsync(string userId) =>
            await _notifications.Find(n => n.UserId == userId && !n.Read).ToListAsync();

        public async Task CreateAsync(Notification notification)
        {
            notification.Timestamp = DateTime.UtcNow;
            notification.Read = false;
            await _notifications.InsertOneAsync(notification);
        }

        public async Task UpdateAsync(string id, Notification notification) =>
            await _notifications.ReplaceOneAsync(n => n.Id == id, notification);

        public async Task DeleteAsync(string id) =>
            await _notifications.DeleteOneAsync(n => n.Id == id);

        public async Task MarkAsReadAsync(string id) =>
            await _notifications.UpdateOneAsync(
                n => n.Id == id,
                Builders<Notification>.Update.Set(n => n.Read, true));
    }
}
