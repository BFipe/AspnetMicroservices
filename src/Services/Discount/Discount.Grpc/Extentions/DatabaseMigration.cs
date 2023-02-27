using Npgsql;

namespace Discount.Grpc.Extentions
{
    public static class DatabaseMigration
    {
        public static async Task MigrateDatabase(IServiceProvider serviceProvider, ILogger logger, int attempt = 0)
        {
            try
            {
                logger.LogInformation("Migrating postgresql data");
                using var scope = serviceProvider.CreateScope();
                var servCollections = scope.ServiceProvider;
                var configurations = servCollections.GetRequiredService<IConfiguration>();
                using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                connection.Open();
                using var command = new NpgsqlCommand { Connection = connection };

                command.CommandText = @"CREATE TABLE IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY, 
                                                        ProductName VARCHAR(24) NOT NULL,
                                                        Description TEXT,
                                                        Amount INT)";
                await command.ExecuteNonQueryAsync();
                logger.LogInformation($"Migrated postresql database.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the postresql database");
                if (attempt < int.Parse(Environment.GetEnvironmentVariable("MaxDatabaseMigrationAttempts") ?? "20"))
                {
                    attempt++;
                    Thread.Sleep(2000);
                    await MigrateDatabase(serviceProvider, logger, attempt);
                }
                else
                {
                    logger.LogCritical("Failed to migrate database (out of attempts), program shutdowns");
                }
            }
        }
    }
}

