using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;

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
    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {



            ShoppingCart cart = request.Cart;
            /// store in db
            /// store in cach



            return new StoreBasketResult(cart.UserName);
        }
    }
}
