
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Walle.Components.MongoDB;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration.Memory;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoDBConfigExtensions
    {
        public static IServiceCollection ConfigureMongoDB(this IServiceCollection services,
            IConfiguration configuration)
        {
            try
            {
                services.AddSingleton<IMongoDBConfig>((provider) =>
                {
                    var mongoDBConfig = new MongoDBConfig
                    {
                        ConnectionStr = configuration["MongoDBConfig:ConnectionStr"].ToString(),
                        DatabaseName = configuration["MongoDBConfig:DatabaseName"].ToString()
                    };
                    return mongoDBConfig;
                });

            }
            catch (Exception ex)
            {
                throw new Exception("exception when load MongoDBConfig{ConnectionStr:\"\",DatabaseName:\"\"}", ex);
            }
            try
            {
                services.AddScoped<IMongoDBClient, MongoDBClient>();
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add scope instance for MongoDBClient.", ex);
            }
            try
            {
                services.AddScopeOfMongDBEntities();
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add scope instance for entiteis based on MongoDBEntity.", ex);
            }

            return services;
        }

        private static void AddScopeOfMongDBEntities(this IServiceCollection services)
        {
            var assembly = Assembly.GetEntryAssembly();
            var mongoEntityTypes = new List<Type>();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type != null && type.BaseType != null && type.BaseType.Equals(typeof(MongoEntity)))
                {
                    mongoEntityTypes.Add(type);
                }
            }

            foreach (var type in mongoEntityTypes)
            {
                var interface_type = typeof(IMongoDBCollection<>).MakeGenericType(type);
                var imple_type = typeof(MongoDBCollection<>).MakeGenericType(type);
                services.AddScoped(interface_type, imple_type);
            }
        }
    }
}