using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("senderId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; }

        [BsonElement("receiverId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReceiverId { get; set; }

        [BsonElement("content")]
        [Required]
        public string Content { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }

        [BsonElement("read")]
        public bool Read { get; set; }

        [BsonElement("auctionId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuctionId { get; set; }
    }
}
