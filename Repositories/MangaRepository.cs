using MongoDB.Driver;

public class MangaRepository : IMangaRepository
{
    private readonly IMongoCollection<Manga> _mangaCollection;

    public MangaRepository(IConfiguration config)
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("Docker_MongoDB"));
        // var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDB_Connection_String"));
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _mangaCollection = database.GetCollection<Manga>(config["MongoDB:CollectionName"]);
    }

    public async Task<List<Manga>> GetAllMangas() =>
        await _mangaCollection.Find(manga => true).ToListAsync();

    public async Task CreateManga(Manga manga) =>
        await _mangaCollection.InsertOneAsync(manga);

    public async Task<Manga> GetMangaById(int mangaId)
    {
        // var filter = Builders<Manga>.Filter.Eq(x => x.MangaId, mangaId);
        return await _mangaCollection.Find(x => x.MangaId == mangaId).FirstOrDefaultAsync();
    }

    public async Task UpdateMangaById(int id, Manga manga) =>
        await _mangaCollection.ReplaceOneAsync(m => m.MangaId == id, manga);

    public async Task DeleteMangaById(int id)
    {
        await _mangaCollection.DeleteOneAsync(m => m.MangaId == id);
    }
}
