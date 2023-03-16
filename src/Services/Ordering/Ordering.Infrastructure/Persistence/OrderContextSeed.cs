using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (orderContext.Orders.Any() == false)
            {
                await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Prebuilded seeded data sucessfully added to database");
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Ilya", LastName = "Ilyusha", EmailAddress = "shilomylo@gmail.com", AddressLine = "Minsk", Country = "Belarus", TotalPrice = 69420 }
            };
        }
    }
}
