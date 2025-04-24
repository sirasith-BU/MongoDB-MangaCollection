using MangaAPI.Models;

namespace MangaAPI.Repositories.interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string username, string password);
        Task CreateUser(User user);
    }
}