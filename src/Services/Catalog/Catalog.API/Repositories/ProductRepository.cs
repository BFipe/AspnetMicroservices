using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _catalogContext
                .Products
                .Find(q => true)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _catalogContext
                .Products
                .Find(q => q.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            return await _catalogContext
             .Products
             .Find(q => q.Category == category)
             .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await _catalogContext
            .Products
            .Find(q => q.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
        }

        public async Task<bool> CreateAsync(Product entity)
        {
            Task task = _catalogContext.Products.InsertOneAsync(entity);
            await task;
            return task.IsCompleted;
        }

        public async Task<bool> UpdateAsync(Product entity)
        {
            var result = await _catalogContext
                .Products
                .ReplaceOneAsync(filter: q => q.Id == entity.Id, replacement: entity);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _catalogContext
            .Products
            .DeleteOneAsync(filter: q => q.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
