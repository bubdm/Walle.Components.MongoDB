namespace Walle.Components.MongoDB
{
    public interface IMongoDBConfig
    {
        string ConnectionStr { get; set; }
        string DatabaseName { get; set; }

        string ToString();
    }
}