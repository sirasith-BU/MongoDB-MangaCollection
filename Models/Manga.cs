using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

    public Manga(CreateMangaDTO createMangaDTO)
    {
        MangaId = Math.Abs(Guid.NewGuid().GetHashCode());
        Title = createMangaDTO.Title;
        Publisher = createMangaDTO.Publisher;
        Type = createMangaDTO.Type;
        ImageUrl = createMangaDTO.ImageUrl;
        Start = createMangaDTO.Start;
        End = createMangaDTO.End;
        NotHave = createMangaDTO.NotHave;
        Description = createMangaDTO.Description;
    }

    public Manga(string id, string type, int mangaId, string title, string publisher, string imageUrl, int start, int end, List<int> notHave, string description)
    {
        Id = id;
        MangaId = mangaId;
        Title = title;
        Publisher = publisher;
        Type = type;
        ImageUrl = imageUrl;
        Start = start;
        End = end;
        NotHave = notHave ?? [];
        Description = description;
    }
}
