using MangaAPI.Models;

namespace MangaAPI.Repositories.interfaces
{
    public interface IMangaRepository
    {
        Task<List<Manga>> GetAllMangas();
        Task CreateManga(Manga manga);
        Task<Manga> GetMangaById(int mangaId);
        Task UpdateMangaById(int id, Manga manga);
        Task DeleteMangaById(int id);
        Task<List<Manga>> SearchAllMangas(string keyword, string type, string publisher);
        Task<List<string>> GetDistinctPublishersAsync();
        Task<List<string>> GetDistinctTypesAsync();
    }
}