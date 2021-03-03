using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Walle.Components.MongoDB
{
    public interface IMongoDBCollection<T> where T : MongoEntity
    {
        string Add(T document);
        IQueryable<T> AsQueryable();
        bool Delete(string id);
        bool Delete(ObjectId id);
        bool DeleteMany(Expression<Func<T, bool>> filter);
        T GetById(string id);
        T GetById(ObjectId id);
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);
        (IEnumerable<T> Result, long Count) Query(IQueryable<T> query, int pageIndex, int pageSize);
        bool UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        bool UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
    }
}