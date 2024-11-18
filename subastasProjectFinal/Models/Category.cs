using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("parentId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentId { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }

}
