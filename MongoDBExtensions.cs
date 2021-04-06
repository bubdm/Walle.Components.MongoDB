using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using Walle.Components.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection ConfigureMongoDB(this IServiceCollection services)
        {
            services.AddMongoDB();
            services.AddMongoDBClientScope();
            services.AddMongoDBEntityScope();
            return services;
        }

        public static IServiceCollection ConfigureMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDB(configuration);
            services.AddMongoDBClientScope();
            services.AddMongoDBEntityScope();
            return services;
        }

        public static void AddMongoDB(this IServiceCollection services)
        {
            try
            {
                IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
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
        }
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
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
        }
        public static void AddMongoDBClientScope(this IServiceCollection services)
        {
            try
            {
                services.AddScoped<IMongoDBClient, MongoDBClient>();
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add scope instance for MongoDBClient.", ex);
            }
        }
        public static void AddMongoDBClientSingleton(this IServiceCollection services)
        {
            try
            {
                services.AddSingleton<IMongoDBClient, MongoDBClient>();
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add scope instance for MongoDBClient.", ex);
            }
        }
        public static void AddMongoDBEntityScope(this IServiceCollection services)
        {
            try
            {
                var entry_assembly = Assembly.GetEntryAssembly();
                var mongoEntityTypes = new List<Type>();
                var types = entry_assembly.GetTypes();
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
                    services.AddSingleton(interface_type, imple_type);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add Singleton instance for entiteis based on MongoDBEntity.", ex);
            }
        }

        public static void AddMongoDBEntitySingleton(this IServiceCollection services)
        {
            try
            {
                var entry_assembly = Assembly.GetEntryAssembly();
                var mongoEntityTypes = new List<Type>();
                var types = entry_assembly.GetTypes();
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
                    services.AddSingleton(interface_type, imple_type);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add Singleton instance for entiteis based on MongoDBEntity.", ex);
            }
        }

        public static void AddMongoDBEntityScope(this IServiceCollection services, Assembly assembly)
        {
            try
            {

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
                    services.AddSingleton(interface_type, imple_type);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add Singleton instance for entiteis based on MongoDBEntity.", ex);
            }
        }

        public static void AddMongoDBEntitySingleton(this IServiceCollection services, Assembly assembly)
        {
            try
            {
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
                    services.AddSingleton(interface_type, imple_type);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("exception when add Singleton instance for entiteis based on MongoDBEntity.", ex);
            }
        }
    }
}