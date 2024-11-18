using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("transactionId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionId { get; set; }

        [BsonElement("amount")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [BsonElement("paymentMethod")]
        [Required]
        public string PaymentMethod { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }

        [BsonElement("payerId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PayerId { get; set; }

        [BsonElement("receiverId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReceiverId { get; set; }
    }
}
