
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;

namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(
                CustomerId.Of(new Guid("a1b2c3d4-1111-4444-8888-123456789001")),
                "Ahmed Hassan",
                "ahmed.hassan@example.com"),

            Customer.Create(
                CustomerId.Of(new Guid("a1b2c3d4-2222-4444-8888-123456789002")),
                "Sara Mohamed",
                "sara.mohamed@example.com")
        };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(
                ProductId.Of(new Guid("b1b2c3d4-1111-4444-8888-123456789001")),
                "MacBook Pro 14",
                1800),

            Product.Create(
                ProductId.Of(new Guid("b1b2c3d4-2222-4444-8888-123456789002")),
                "Dell XPS 15",
                1500),

            Product.Create(
                ProductId.Of(new Guid("b1b2c3d4-3333-4444-8888-123456789003")),
                "iPad Air",
                700),

            Product.Create(
                ProductId.Of(new Guid("b1b2c3d4-4444-4444-8888-123456789004")),
                "Samsung Galaxy S24",
                900)
        };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of(
                "Ahmed",
                "Hassan",
                "ahmed.hassan@example.com",
                "Nasr City, Street 10",
                "Egypt",
                "Cairo",
                "11765");

            var address2 = Address.Of(
                "Sara",
                "Mohamed",
                "sara.mohamed@example.com",
                "Smouha, Street 15",
                "Egypt",
                "Alexandria",
                "21648");

            var payment1 = Payment.Of(
                "Ahmed Hassan",
                "4111111111111111",
                "12/28",
                "123",
                1);

            var payment2 = Payment.Of(
                "Sara Mohamed",
                "5555555555554444",
                "06/29",
                "456",
                2);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("a1b2c3d4-1111-4444-8888-123456789001")),
                OrderName.Of("ORD01"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1);

            order1.Add(
                ProductId.Of(new Guid("b1b2c3d4-1111-4444-8888-123456789001")),
                1,
                1800);

            order1.Add(
                ProductId.Of(new Guid("b1b2c3d4-3333-4444-8888-123456789003")),
                2,
                700);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("a1b2c3d4-2222-4444-8888-123456789002")),
                OrderName.Of("ORD02"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2);

            order2.Add(
                ProductId.Of(new Guid("b1b2c3d4-2222-4444-8888-123456789002")),
                1,
                1500);

            order2.Add(
                ProductId.Of(new Guid("b1b2c3d4-4444-4444-8888-123456789004")),
                1,
                900);

            return new List<Order> { order1, order2 };
        }
    }
}
