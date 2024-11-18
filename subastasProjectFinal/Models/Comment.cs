using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("auctionId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuctionId { get; set; }

        [BsonElement("userId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("content")]
        [Required]
        public string Content { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }

        [BsonElement("likes")]
        public int Likes { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}
