namespace Catalog.API.Data
{
    public static class DataService
    {
        public static IServiceCollection AddDataServices(this IServiceCollection service)
        {
            service.AddScoped<ICatalogContext, CatalogContext>();
            return service;
        }
    }
}
