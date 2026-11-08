using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingApplication;
using Ordering.Domain.ValueObject;
using Ordering.Domain.Models;
using System.Reflection;

namespace Ordering.InfraStructure.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext
            (DbContextOptions<ApplicationDBContext> options) 
            : base(options)
        {



        }

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> orderItems => Set<OrderItem>();



        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly
                (Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

    }
}
