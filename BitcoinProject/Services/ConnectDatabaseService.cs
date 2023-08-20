using BitcoinProject.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BitcoinProject.Services
{
    public class ConnectDatabaseService : IConnectDatabaseService
    {
        private IDatabaseOptions _DatabaseOptions;

        public ConnectDatabaseService(
            IDatabaseOptions databaseOptions)
        {
            _DatabaseOptions = databaseOptions;
        }

        public IMongoCollection<BsonDocument> ConnectDatabase()
        {
            MongoClient mongoClient = new MongoClient(_DatabaseOptions.ConnectionString);
            IMongoDatabase db = mongoClient.GetDatabase(_DatabaseOptions.Database);
            return db.GetCollection<BsonDocument>("bitcoinCollection");
        }
    }
}
