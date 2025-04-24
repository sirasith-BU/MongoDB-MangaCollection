using MongoDB.Driver;
using MangaAPI.Models;
using MangaAPI.Repositories.interfaces;
using MangaAPI.Core;

namespace MangaAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(MongoDBContext mongoDBContext)
        {
            _userCollection = mongoDBContext.Users;
        }

        public async Task CreateUser(User user)
            => await _userCollection.InsertOneAsync(user);

        public async Task<User> Login(string username, string password)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(u => u.Username, username),
                Builders<User>.Filter.Eq(u => u.Password, password)
            );
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
