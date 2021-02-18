# Walle.Components.MongoDB

[TOC]

## 安装

### 使用 Package Manager:

```
Install-Package Walle.Components.MongoDB -Version 1.0.0
```

### 使用.NetCore CLI:

```
dotnet add package Walle.Components.MongoDB --version 1.0.0
```

## 示例

### 配置

1. 在 ` appsettings.json` 中添加以下节点:

```
{
  "MongoDBConfig": {
    "ConnectionStr": "mongodb://127.0.0.1:27017/walle",
    "DatabaseName": "walle"
  }
}
```

2. 在 `Startup.cs` 中注册依赖:

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

### 实体示例

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

### 调用示例

1. 使用 `IMongoDBCollection<T>` 的 Resolve 实例来完成各种 `CRUD` 的操作。

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

2. 使用 `IMongoDBClient` 的 Resolve 实例来完成 `CRUD`:

```cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Walle.Components.MongoDB;

namespace OPPO.SDL.Compliance.WebAPI.Controllers
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
