using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Newsletter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BsonElement("subscribed")]
        public bool Subscribed { get; set; }

        [BsonElement("subscribedAt")]
        [BsonDateTimeOptions]
        public DateTime SubscribedAt { get; set; }

        [BsonElement("unsubscribedAt")]
        [BsonDateTimeOptions]
        public DateTime? UnsubscribedAt { get; set; }

        [BsonElement("preferences")]
        public List<string> Preferences { get; set; }
    }
}
