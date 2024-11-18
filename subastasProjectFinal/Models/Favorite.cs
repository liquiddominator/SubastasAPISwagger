using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Favorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("auctionId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuctionId { get; set; }

        [BsonElement("addedAt")]
        [BsonDateTimeOptions]
        public DateTime AddedAt { get; set; }
    }
}
