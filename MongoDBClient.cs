using System;
using System.Security.Authentication;
using MongoDB.Driver;

namespace Walle.Components.MongoDB
{
    public class MongoDBClient : IMongoDBClient
    {
        protected MongoClient client = null;
        private IMongoDBConfig MongoDBConfig { get; }
        public MongoDBClient(IMongoDBConfig config)
        {
            this.MongoDBConfig = config;
            try
            {
                string ConnectStr = MongoDBConfig.ConnectionStr;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectStr));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                client = new MongoClient(settings);
            }
            catch (Exception ex)
            {
                throw new Exception("MongoDB Connect Failure.", ex);
            }
        }
        public IMongoDatabase GetDefaultDataBase()
        {
            var name = MongoDBConfig.DatabaseName;
            return client.GetDatabase(name);
        }
        public IMongoCollection<T> GetDefaultCollection<T>()
        {
            var database = GetDefaultDataBase();
            var type = typeof(T);
            var collectionName = type.Name;
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
