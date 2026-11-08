using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.InfraStructure.Data;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(
            this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<ApplicationDBContext>();

             dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

           await seedAsync(dbContext);
        }

        private static async Task seedAsync(ApplicationDBContext dbContext)
        {
            await SeedcustomerAsync(dbContext);
            await SeedProductAsync(dbContext);
            await SeedOrdersAndItemsAsync(dbContext);

        }

        private static async Task SeedcustomerAsync(ApplicationDBContext dbContext)
        {

            if(!await dbContext.Customers.AnyAsync())
            {
                await dbContext.AddRangeAsync(InitialData.Customers);
                await dbContext.SaveChangesAsync();
            }

        }

        private static async Task SeedProductAsync(ApplicationDBContext dbContext)
        {

            if (!await dbContext.Products.AnyAsync())
            {
                await dbContext.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }

        }


        private static async Task SeedOrdersAndItemsAsync(ApplicationDBContext dbContext)
        {

            if (!await dbContext.orderItems.AnyAsync())
            {
                await dbContext.AddRangeAsync(InitialData.OrdersWithItems);
                await dbContext.SaveChangesAsync();
            }

        }


    }
}