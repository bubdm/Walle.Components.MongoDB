using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Walle.Components.MongoDB
{
    public class MongoDBCollection<T> : IMongoDBCollection<T> where T : MongoEntity
    {
        private IMongoDBClient MongoDBClient { get; }

        public MongoDBCollection(IMongoDBClient mongoDBClient)
        {
            this.MongoDBClient = mongoDBClient;
        }

        public T GetById(string id)
        {
            try
            {
                var result = MongoDBClient.Find<T>(p => p.ObjectID == new ObjectId(id)).FirstOrDefault();
                return result;
            }
            catch
            {
                return default(T);
            }
        }
        public T GetById(ObjectId id)
        {
            try
            {
                var result = MongoDBClient.Find<T>(p => p.ObjectID == id).FirstOrDefault();
                return result;
            }
            catch
            {
                return default(T);
            }
        }
        public string Add(T document)
        {
            MongoDBClient.Insert(document);
            return document.ObjectID.ToString();
        }
        public bool Delete(string id)
        {
            var result = MongoDBClient.DeleteOne<T>(p => p.ObjectID == new ObjectId(id));
            return result;
        }
        public bool DeleteMany(Expression<Func<T, bool>> filter)
        {
            var result = MongoDBClient.DeleteMany<T>(filter);
            return result;
        }
        public bool Delete(ObjectId id)
        {
            var result = MongoDBClient.DeleteOne<T>(p => p.ObjectID == id);
            return result;
        }
        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            var result = MongoDBClient.Find<T>(filter);
            return result;
        }
        public IQueryable<T> AsQueryable()
        {
            var col = MongoDBClient.GetDefaultCollection<T>();
            return col.AsQueryable();
        }
        public (IEnumerable<T> Result, long Count) Query(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var count = query.Count<T>();
            if (pageIndex != 0 && pageSize != 0)
            {
                query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            }
            var result = query.ToList();
            return (result, count);
        }
        public bool UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            return MongoDBClient.UpdateOne<T>(filter, update);
        }
    }
}
