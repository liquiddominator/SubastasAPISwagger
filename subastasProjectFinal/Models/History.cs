using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class History
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("action")]
        [Required]
        public string Action { get; set; }

        [BsonElement("details")]
        public string Details { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }

        [BsonElement("ip")]
        [Required]
        public string IP { get; set; }
    }
}
