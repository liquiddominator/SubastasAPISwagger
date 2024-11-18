using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace subastasProjectFinal.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [BsonElement("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BsonElement("password")]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [BsonElement("firstName")]
        [Required]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        [Required]
        public string LastName { get; set; }

        [BsonElement("phoneNumber")]
        [Phone]
        public string PhoneNumber { get; set; }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }

        [BsonElement("lastLogin")]
        [BsonDateTimeOptions]
        public DateTime LastLogin { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("verifiedEmail")]
        public bool VerifiedEmail { get; set; }
    }

    public class Address
    {
        [BsonElement("street")]
        [Required]
        public string Street { get; set; }

        [BsonElement("city")]
        [Required]
        public string City { get; set; }

        [BsonElement("state")]
        [Required]
        public string State { get; set; }

        [BsonElement("country")]
        [Required]
        public string Country { get; set; }

        [BsonElement("zipCode")]
        [Required]
        public string ZipCode { get; set; }
    }
}
