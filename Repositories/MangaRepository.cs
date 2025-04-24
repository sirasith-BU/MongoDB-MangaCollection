using MangaAPI.Core;
using MangaAPI.Models;
using MangaAPI.Repositories.interfaces;
using MongoDB.Driver;

namespace MangaAPI.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly IMongoCollection<Manga> _mangaCollection;

        public MangaRepository(MongoDBContext mongoDBContext)
        {
            _mangaCollection = mongoDBContext.Mangas;
        }
        public async Task<List<Manga>> SearchAllMangas(string searchText, string type, string publisher)
        {
            var filters = new List<FilterDefinition<Manga>>();

            if (!string.IsNullOrEmpty(searchText))
            {
                filters.Add(Builders<Manga>.Filter.And(
                Builders<Manga>.Filter.Where(g =>
                string.IsNullOrEmpty(searchText) ||
                g.MangaId.ToString().Contains(searchText) ||
                g.Title.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                g.Description.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                )
            ));
            }
            if (!string.IsNullOrEmpty(type))
            {
                filters.Add(Builders<Manga>.Filter.Eq(g => g.Type, type));
            }
            if (!string.IsNullOrEmpty(publisher))
            {
                filters.Add(Builders<Manga>.Filter.Eq(g => g.Publisher, publisher));
            }

            var finalFilter = filters.Any()
                ? Builders<Manga>.Filter.And(filters)
                : Builders<Manga>.Filter.Empty;

            return await _mangaCollection.Find(finalFilter).ToListAsync();
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
        public async Task<List<string>> GetDistinctPublishersAsync()
        {
            return await _mangaCollection.Distinct<string>("Publisher", FilterDefinition<Manga>.Empty).ToListAsync();
        }
        public async Task<List<string>> GetDistinctTypesAsync()
        {
            return await _mangaCollection.Distinct<string>("Type", FilterDefinition<Manga>.Empty).ToListAsync();
        }
    }
}
