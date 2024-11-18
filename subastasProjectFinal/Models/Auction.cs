using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace subastasProjectFinal.Models
{
    public class Auction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        [Required]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("sellerId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SellerId { get; set; }

        [BsonElement("startingPrice")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal StartingPrice { get; set; }

        [BsonElement("currentPrice")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal CurrentPrice { get; set; }

        [BsonElement("startDate")]
        [Required]
        [BsonDateTimeOptions]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        [Required]
        [BsonDateTimeOptions]
        public DateTime EndDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("images")]
        public List<string> Images { get; set; }

        [BsonElement("condition")]
        public string Condition { get; set; }

        [BsonElement("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [BsonElement("city")]
        [Required]
        public string City { get; set; }

        [BsonElement("country")]
        [Required]
        public string Country { get; set; }
    }
}
