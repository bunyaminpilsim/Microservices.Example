using MongoDB.Driver;

namespace Stock.API.Services
{
    public class MongoDBService
    {
        readonly IMongoDatabase _mongoDatabase;

        public MongoDBService(IConfiguration configuration)
        {
            MongoClient client = new(configuration.GetConnectionString("MongoDB"));
            _mongoDatabase = client.GetDatabase("StocAPIDB");
        }

        public IMongoCollection<T> GetCollection<T>() => _mongoDatabase.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}
