using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Walle.Components.MongoDB
{
    public static class MongoDBClientExtensions
    {
        public static void Insert<T>(this IMongoDBClient dbclient, T obj)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            collection.InsertOne(obj);
        }
        public static IEnumerable<T> Find<T>(this IMongoDBClient dbclient, Expression<Func<T, bool>> filter)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.Find<T>(filter);
            return result?.ToList();
        }
        public static bool UpdateOne<T>(this IMongoDBClient dbclient, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.UpdateOne<T>(filter, update);
            return result.IsAcknowledged;
        }
        public static bool UpdateMany<T>(this IMongoDBClient dbclient, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.UpdateMany<T>(filter, update);
            return result.IsAcknowledged;
        }
        public static bool DeleteOne<T>(this IMongoDBClient dbclient, Expression<Func<T, bool>> filter)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.DeleteOne<T>(filter);
            return true;
        }
        public static bool DeleteMany<T>(this IMongoDBClient dbclient, Expression<Func<T, bool>> filter)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.DeleteMany<T>(filter);
            return true;
        }
        public static (List<T> Result, long Count) Page<T>(this IQueryable<T> query, int pageIndex = 0, int pageSize = 0)
        {
            var count = query.Count<T>();
            if (pageIndex > 0 && pageSize > 0)
            {
                query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            }
            var result = query.ToList();
            return (result, count);
        }
        public static (List<T> Result, long Count) Page<T>(this IQueryable<T> query, int pageIndex = 0, int pageSize = 0, Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            var count = query.Count<T>();
            if (pageIndex > 0 && pageSize > 0)
            {
                query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            }
            var result = query.ToList();
            return (result, count);
        }
        public static (List<T> Result, long Count) ExcuteQuery<T>(this IQueryable<T> query)
        {
            var count = query.Count<T>();
            var result = query.ToList();
            return (result, count);
        }

        public static UpdateDefinition<T> UpdateBuilder<T>(this T t) where T : IMongoEntity
        {
            return Builders<T>.Update.Set(p => p.ID, t.ID);
        }
    }
}