using Basket.API.Entities;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ShoppingCart shoppingCart = new();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddStackExchangeRedisCache(q => 
            {
                //q.Configuration = Environment.GetEnvironmentVariable("RedisConnection");
                q.Configuration = "localhost:6379";
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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