using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDb"));
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName"));

            Products = database.GetCollection<Product>(Environment.GetEnvironmentVariable("CollectionName"));
            CatalogContextSeed.InsertDefaultData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
