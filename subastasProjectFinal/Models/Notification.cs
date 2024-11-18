using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("type")]
        [Required]
        public string Type { get; set; }

        [BsonElement("message")]
        [Required]
        public string Message { get; set; }

        [BsonElement("relatedId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RelatedId { get; set; }

        [BsonElement("read")]
        public bool Read { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions]
        public DateTime Timestamp { get; set; }
    }
}
