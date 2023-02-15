using Catalog.API.Data;
using System.Runtime.CompilerServices;

namespace Catalog.API.Repositories
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddDataServices();
            return service;
        }
    }
}
