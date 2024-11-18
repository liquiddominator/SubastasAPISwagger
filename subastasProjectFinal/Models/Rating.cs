using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("fromUserId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FromUserId { get; set; }

        [BsonElement("toUserId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ToUserId { get; set; }

        [BsonElement("auctionId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuctionId { get; set; }

        [BsonElement("rating")]
        [Required]
        [Range(1, 5)]
        public int RatingValue { get; set; }

        [BsonElement("comment")]
        public string Comment { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }
    }
}
