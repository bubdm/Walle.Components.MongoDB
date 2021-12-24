using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Walle.Components.MongoDB
{
    [Serializable]
    public abstract class MongoEntity : IMongoEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get => ObjectID.ToString(); set => ObjectID = new ObjectId(value); }

        /// <summary>
        /// Object ID
        /// </summary>
        [BsonId]
        [JsonIgnore]
        [IgnoreDataMember]
        public ObjectId ObjectID { get; set; }

        /// <summary>
        /// Object Tags
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
