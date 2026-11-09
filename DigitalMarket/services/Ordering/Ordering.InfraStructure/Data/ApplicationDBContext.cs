using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;
using OrderingApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

    public class ApplicationDBContext : DbContext , IApplicationDbContext
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

    public DbSet<OrderItem> OrderItems => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly
                (Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

    }

