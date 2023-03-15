using Discount.API.Extentions;
using Discount.API.Repositories;

namespace Discount.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddLogging();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

            //builder.Services.AddApplicationServices();
            //builder.Services.AddInfrastructureServices();

            var app = builder.Build();

            var serviceProvider = app.Services;
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            await DatabaseMigration.MigrateDatabase(serviceProvider, logger);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}