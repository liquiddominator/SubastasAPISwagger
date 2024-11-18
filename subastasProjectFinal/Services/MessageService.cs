using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageService(IMongoDatabase database)
        {
            _messages = database.GetCollection<Message>("Messages");
        }

        public async Task<List<Message>> GetAllAsync() =>
            await _messages.Find(_ => true).ToListAsync();

        public async Task<Message> GetByIdAsync(string id) =>
            await _messages.Find(message => message.Id == id).FirstOrDefaultAsync();

        public async Task<List<Message>> GetBySenderIdAsync(string senderId) =>
            await _messages.Find(message => message.SenderId == senderId).ToListAsync();

        public async Task<List<Message>> GetByReceiverIdAsync(string receiverId) =>
            await _messages.Find(message => message.ReceiverId == receiverId).ToListAsync();

        public async Task<List<Message>> GetConversationAsync(string user1Id, string user2Id) =>
            await _messages.Find(m =>
                    (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                    (m.SenderId == user2Id && m.ReceiverId == user1Id))
                .SortByDescending(m => m.Timestamp)
                .ToListAsync();

        public async Task CreateAsync(Message message)
        {
            message.Timestamp = DateTime.UtcNow;
            message.Read = false;
            await _messages.InsertOneAsync(message);
        }

        public async Task UpdateAsync(string id, Message message) =>
            await _messages.ReplaceOneAsync(m => m.Id == id, message);

        public async Task DeleteAsync(string id) =>
            await _messages.DeleteOneAsync(m => m.Id == id);

        public async Task MarkAsReadAsync(string id) =>
            await _messages.UpdateOneAsync(
                m => m.Id == id,
                Builders<Message>.Update.Set(m => m.Read, true));
    }
}
