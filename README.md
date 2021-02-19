# Walle.Components.MongoDB

happy code-first using MongoDB

## Installations

### Package Manager:

```
Install-Package Walle.Components.MongoDB -Version 2.0.0
```

### .NetCore CLI:

```
dotnet add package Walle.Components.MongoDB --version 2.0.0
```

### Configurations

1. add the follow section into ` appsettings.json` :

```
{
  "MongoDBConfig": {
    "ConnectionStr": "mongodb://127.0.0.1:27017/walle",
    "DatabaseName": "walle"
  }
}
```

2. register dependency in your `Startup.cs` 

```csharp

private IConfiguration Configuration { get; }
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

public void ConfigureServices(IServiceCollection services)
{
    services.ConfigureMongoDB(Configuration);
}

```

## Samples

1. Inherited an entity from MongoEntity which will be a collection model.
```cs
using Walle.Components.MongoDB;
namespace Demo
{
    public class ActivityModel : MongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
```

2. using `IMongoDBCollection<T>` to do ```IQueryable``` operations with LINQ.

```cs
using Demo;
using Walle.Components.MongoDB;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IMongoDBCollection<ActivityModel> ActivityCollection { get; }
        public ActivityController(IMongoDBCollection<ActivityModel> collection)
        {
            this.ActivityCollection = collection;
        }

        [HttpGet]
        public RespList<ActivityModel> GetAll()
        {
            RespList<ActivityModel> resp = new RespList<ActivityModel>();
            try
            {
                var sources = ActivityCollection.AsQueryable()?.ToList();
                resp.Message = $"success.";
                resp.Data = sources;
            }
            catch (Exception ex)
            {
                resp.Code = (int)RespCode.Exception;
                resp.IsSuccess = false;
            }
            return resp;
        }
    }
}

```

3. using `IMongoDBClient` to get the mongodb client for more operations.

```cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Walle.Components.MongoDB;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private IMongoDBClient Client { get; }

        public DemoController(IMongoDBClient Client)
        {
            this.Client = Client;
        }

        [HttpGet]
        public RespList<ActivityModel> GetAll()
        {
            RespList<ActivityModel> resp = new RespList<ActivityModel>();
            try
            {
                var results = Client.Find<ActivityModel>(p => p.Name == "Misaya").ToList();
                resp.Data = results;
            }
            catch (Exception )
            {
                var msg = "Exception occured when query activity.";
            }
            return resp;
        }

    }
}

```
