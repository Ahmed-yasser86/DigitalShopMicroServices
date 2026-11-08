using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.InfraStructure.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure
            (EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi=>oi.Id);
            builder.Property(c => c.Id)
                .HasConversion(OrderItemId =>
                OrderItemId.Value, dbId =>
                OrderItemId.Of(dbId));

            builder.HasOne<Product>().
                WithMany().
                HasForeignKey(pi=>pi.ProductId);

            builder.Property(oi => oi.Price).IsRequired();
            builder.Property(oi => oi.Quantity).IsRequired();
        }


        


    }
}
