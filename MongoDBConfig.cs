using System;

namespace Walle.Components.MongoDB
{
    public class MongoDBConfig : IMongoDBConfig
    {
        private string connectStr = string.Empty;
        private string dbName = string.Empty;
        public string ConnectionStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(connectStr))
                {
                    throw new ArgumentNullException("MongoDBConfig:ConnectionStr Is Null.");
                }
                return connectStr;
            }
            set
            {
                this.connectStr = value;
            }
        }
        public string DatabaseName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(dbName))
                {
                    throw new ArgumentNullException("MongoDBConfig:DatabaseName Is Null.");
                }
                return dbName;
            }
            set
            {
                this.dbName = value;
            }
        }

        public override string ToString()
        {
            return $"ConnectionString:{ConnectionStr};DataBaseName:{DatabaseName}";
        }
    }
}
