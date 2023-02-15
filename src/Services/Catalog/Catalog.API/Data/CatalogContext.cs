using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("CollectionName"));
            CatalogContextSeed.InsertDefaultData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
