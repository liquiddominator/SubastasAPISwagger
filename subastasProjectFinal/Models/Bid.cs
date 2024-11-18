using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Bid
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("auctionId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuctionId { get; set; }

        [BsonElement("bidderId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BidderId { get; set; }

        [BsonElement("amount")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [BsonElement("timestamp")]
        [Required]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}
