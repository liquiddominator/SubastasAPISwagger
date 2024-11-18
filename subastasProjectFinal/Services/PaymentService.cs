using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class PaymentService
    {
        private readonly IMongoCollection<Payment> _payments;

        public PaymentService(IMongoDatabase database)
        {
            _payments = database.GetCollection<Payment>("Payments");
        }

        public async Task<List<Payment>> GetAllAsync() =>
            await _payments.Find(_ => true).ToListAsync();

        public async Task<Payment> GetByIdAsync(string id) =>
            await _payments.Find(payment => payment.Id == id).FirstOrDefaultAsync();

        public async Task<List<Payment>> GetByPayerIdAsync(string payerId) =>
            await _payments.Find(payment => payment.PayerId == payerId).ToListAsync();

        public async Task<List<Payment>> GetByReceiverIdAsync(string receiverId) =>
            await _payments.Find(payment => payment.ReceiverId == receiverId).ToListAsync();

        public async Task CreateAsync(Payment payment)
        {
            payment.Timestamp = DateTime.UtcNow;
            await _payments.InsertOneAsync(payment);
        }

        public async Task UpdateAsync(string id, Payment payment) =>
            await _payments.ReplaceOneAsync(p => p.Id == id, payment);

        public async Task DeleteAsync(string id) =>
            await _payments.DeleteOneAsync(p => p.Id == id);

        public async Task UpdateStatusAsync(string id, string status) =>
            await _payments.UpdateOneAsync(
                p => p.Id == id,
                Builders<Payment>.Update.Set(p => p.Status, status));
    }
}
