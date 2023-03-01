using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using System.Net;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddStackExchangeRedisCache(q =>
            {
                q.Configuration = Environment.GetEnvironmentVariable("RedisConnection");
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();


            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(q =>
            {
                q.Address = new Uri(Environment.GetEnvironmentVariable("DiscountGrpcUri") ?? throw new Exception("Null DiscountGrpcUri"));
            });
            builder.Services.AddScoped<DiscountGrpcService>();

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