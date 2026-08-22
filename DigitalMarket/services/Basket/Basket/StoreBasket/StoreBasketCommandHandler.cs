using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;
using FluentValidation;
using MediatR;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart Cart) : Icommand<StoreBasketResult>;
    public record StoreBasketResult (string UserName);


    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {

        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");

        }

    }
    public class StoreBasketCommandHandler(IBasketRepository Repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {

        public async Task AquireDiscount(StoreBasketCommand request)
        {

            foreach (var CartItem in request.Cart.Items)
            {

                var copun = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = CartItem.ProductName });
                CartItem.Price -= copun.Amount;

            }
        }
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {



            await AquireDiscount(request);


            await Repository.StoreBasket(request.Cart, cancellationToken);

            return new StoreBasketResult(request.Cart.UserName);
        }
    }
}
