using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public async Task<Coupon> GetDiscount(string productName)
        {
            using (var connection = new NpgsqlConnection("Host=discount.postgresdb;Port=5432;Database=DiscountDb;User Id=admin;Password=admin1212;"))
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                    ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});
                if (coupon is null)
                {
                    return new Coupon
                    {
                        ProductName = "No discount",
                        Amount = 1,
                        Description = "No discount specified",
                    };
                }             

                return coupon;
            }
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection("Host=discount.postgresdb;Port=5432;Database=DiscountDb;User Id=admin;Password=admin1212;"))
            {
                var result = await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { 
                        ProductName = coupon.ProductName, 
                        Description = coupon.Description,
                        Amount = coupon.Amount,
                    });

                return result != 0;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection("Host=discount.postgresdb;Port=5432;Database=DiscountDb;User Id=admin;Password=admin1212;"))
            {
                var result = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@Productname, Description=@Description, Amount=@Amount WHERE Id=@Id",
                    new
                    {
                        Id = coupon.Id,
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount,
                    });

                return result != 0;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using (var connection = new NpgsqlConnection("Host=discount.postgresdb;Port=5432;Database=DiscountDb;User Id=admin;Password=admin1212;"))
            {
                var result = await connection.ExecuteAsync
                    ("DELETE FROM Coupon WHERE Productname = @ProductName",
                    new
                    {
                        ProductName = productName,
                    });

                return result != 0;
            }
        }
    }
}
