using MangaAPI.Dto;
using MangaAPI.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MangaAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("firstname")]
        public string Firstname { get; set; } = string.Empty;

        [BsonElement("lastname")]
        public string Lastname { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        public User(CreateUserDTO newUser)
        {
            UserId = Math.Abs(Guid.NewGuid().GetHashCode());
            Firstname = newUser.Firstname;
            Lastname = newUser.Lastname;
            Username = newUser.Username;
            Password = newUser.Password.ToSHA256Hash();
            CreatedAt = DateTime.Now;
        }
    }
}