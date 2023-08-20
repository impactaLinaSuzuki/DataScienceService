using MongoDB.Bson;
using MongoDB.Driver;

namespace BitcoinProject.Interfaces
{
    public interface IConnectDatabaseService
    {
        IMongoCollection<BsonDocument> ConnectDatabase();
    }
}