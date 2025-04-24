using MangaAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MangaAPI.Core
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration config)
        {
            // var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDB_Connection_String"));
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<Manga> Mangas => _database.GetCollection<Manga>("manga");
        public IMongoCollection<User> Users => _database.GetCollection<User>("user");
    }
}