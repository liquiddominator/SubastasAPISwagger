using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("reporterId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReporterId { get; set; }

        [BsonElement("reportedUserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReportedUserId { get; set; }

        [BsonElement("auctionId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuctionId { get; set; }

        [BsonElement("reason")]
        [Required]
        public string Reason { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }
    }
}
