public interface IMangaRepository
{
    Task<List<Manga>> GetAllMangas();
    Task CreateManga(Manga manga);
    Task<Manga> GetMangaById(int mangaId);
    Task UpdateMangaById(int id, Manga manga);
    Task DeleteMangaById(int id);
}