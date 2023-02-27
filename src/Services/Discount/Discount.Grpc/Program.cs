using Discount.Grpc.Extentions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

namespace Company.WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            var serviceProvider = app.Services;
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            await DatabaseMigration.MigrateDatabase(serviceProvider, logger);

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}