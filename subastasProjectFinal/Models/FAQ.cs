using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class FAQ
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("question")]
        [Required]
        public string Question { get; set; }

        [BsonElement("answer")]
        [Required]
        public string Answer { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("order")]
        public int Order { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}
