using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {

        public DbSet<Coupon> Coupons { set; get; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "MacBook Air M3",
                    Description = "MacBook Air Discount",
                    Amount = 200
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Samsung Galaxy S24",
                    Description = "Samsung Galaxy Discount",
                    Amount = 150
                }
            );
        }
    }
}
