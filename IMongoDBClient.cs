using MongoDB.Driver;

namespace Walle.Components.MongoDB
{
    public interface IMongoDBClient
    {
        IMongoDatabase GetDefaultDataBase();
        IMongoCollection<T> GetDefaultCollection<T>();
    }
}