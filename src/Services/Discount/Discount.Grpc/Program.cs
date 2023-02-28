using Discount.Grpc.Extentions;
using Discount.Grpc.Mapper;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

namespace Company.WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(DiscountProfile));
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            var app = builder.Build();


            var serviceProvider = app.Services;
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            await DatabaseMigration.MigrateDatabase(serviceProvider, logger);

            // Configure the HTTP request pipeline.

            app.MapGrpcService<DiscountService>();

            app.Run();
        }
    }
}