using MangaAPI.Dto;
using MangaAPI.Models;
using MangaAPI.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class MangaController : ControllerBase
    {
        private readonly IMangaRepository _mangaRepository;

        public MangaController(IMangaRepository MangaRepository)
        {
            _mangaRepository = MangaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Manga>>> GetMangas()
        {
            try
            {
                var mangas = await _mangaRepository.GetAllMangas();
                var mangaDTOs = mangas.Select(manga => new
                {
                    manga.MangaId,
                    manga.Title,
                    manga.Publisher,
                    manga.ImageUrl,
                    manga.Description
                }).ToList();

                return Ok(new { success = true, message = "Load Manga Successful", data = mangaDTOs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Manga>>> GetMangaAsync(int id)
        {
            try
            {
                var manga = await _mangaRepository.GetMangaById(id);
                if (manga == null)
                {
                    return NotFound("MangaId: " + id + " not found");
                }
                var mangas = new List<Manga> { manga };

                var mangaDTOs = mangas.Select(manga => new
                {
                    manga.MangaId,
                    manga.Title,
                    manga.Publisher,
                    manga.Type,
                    manga.ImageUrl,
                    manga.Start,
                    manga.End,
                    manga.NotHave,
                    manga.Description
                }).ToList();

                return Ok(new { success = true, message = "Get " + manga.Title + " Async", data = mangaDTOs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Manga>>> SearchMangas(
        [FromQuery] string? keyword,
        [FromQuery] string? type,
        [FromQuery] string? publisher)
        {
            try
            {
                var mangas = await _mangaRepository.SearchAllMangas(keyword!, type!, publisher!);
                var mangaDTOs = mangas.Select(manga => new
                {
                    manga.MangaId,
                    manga.Title,
                    manga.Publisher,
                    manga.ImageUrl,
                    manga.Description
                }).ToList();

                return Ok(new { success = true, message = "Search Manga Successful", data = mangaDTOs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<string>>> DistinctTypes()
        {
            try
            {
                List<string> types = await _mangaRepository.GetDistinctTypesAsync();
                return Ok(new { success = true, messeage = "Get type successful", data = types });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("publishers")]
        public async Task<ActionResult<List<string>>> DistinctPublishers()
        {
            try
            {
                List<string> publishers = await _mangaRepository.GetDistinctPublishersAsync();
                return Ok(new { success = true, messeage = "Get publisher successful", data = publishers });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] CreateMangaDTO newMangaData)
        {
            try
            {
                var newManga = new Manga(newMangaData);
                newManga.CreatedBy = 0;

                await _mangaRepository.CreateManga(newManga);
                return Ok(new { success = true, message = "Create " + newMangaData.Title + " successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManga(int id, [FromBody] UpdateMangaDTO newMangaData)
        {
            try
            {
                var manga = await _mangaRepository.GetMangaById(id);
                if (manga == null)
                {
                    return NotFound("MangaId: " + id + " not found");
                }

                manga.Title = newMangaData.Title ?? manga.Title;
                manga.Publisher = newMangaData.Publisher ?? manga.Publisher;
                manga.Type = newMangaData.Type ?? manga.Type;
                manga.ImageUrl = newMangaData.ImageUrl ?? manga.ImageUrl;
                manga.Start = newMangaData.Start != 0 ? newMangaData.Start : 1;
                manga.End = newMangaData.End != 0 ? newMangaData.End : 1;
                manga.NotHave = newMangaData.NotHave ?? manga.NotHave;
                manga.Description = newMangaData.Description ?? manga.Description;
                manga.UpdatedAt = DateTime.Now;

                await _mangaRepository.UpdateMangaById(id, manga);
                return Ok(new { success = true, message = "Update " + manga.Title + " successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManga(int id)
        {
            try
            {
                await _mangaRepository.DeleteMangaById(id);
                return Ok(new { success = true, message = "Delete manga successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
