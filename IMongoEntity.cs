using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Walle.Components.MongoDB
{
    public interface IMongoEntity
    {
        string ID { get; set; }
        ObjectId ObjectID { get; set; }
        List<string> Tags { get; set; }
        DateTime CreationTime { get; }
    }
}