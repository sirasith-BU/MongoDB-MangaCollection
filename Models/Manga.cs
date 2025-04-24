using MangaAPI.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MangaAPI.Models
{
    public class Manga
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("mangaId")]
        public int MangaId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("publisher")]
        public string Publisher { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("start")]
        public int Start { get; set; }

        [BsonElement("end")]
        public int End { get; set; }

        [BsonElement("notHave")]
        public List<int> NotHave { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("createdBy")]
        public int CreatedBy { get; set; }

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; }

        public Manga(CreateMangaDTO createMangaDTO)
        {
            int newId = Math.Abs(Guid.NewGuid().GetHashCode());
            MangaId = newId;
            Title = createMangaDTO.Title;
            Publisher = createMangaDTO.Publisher;
            Type = createMangaDTO.Type;
            ImageUrl = createMangaDTO.ImageUrl;
            Start = createMangaDTO.Start;
            End = createMangaDTO.End;
            NotHave = createMangaDTO.NotHave;
            Description = createMangaDTO.Description;
            CreatedAt = DateTime.Now;
        }
    }
}
