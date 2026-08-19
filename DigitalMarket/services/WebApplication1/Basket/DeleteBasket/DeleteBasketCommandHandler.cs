using Basket.API.Data;
using FluentValidation;
using JasperFx.Events.Daemon;

namespace Basket.API.Basket.DeleteBasket
{

    public record DeleteBasketCommand(string UserName) : Icommand<DeleteBasketResult>;
        
    public record DeleteBasketResult(bool IsSuccess);


    public class DeleteBasketValidatore
        : AbstractValidator<DeleteBasketCommand>
    {
        public  DeleteBasketValidatore() {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        } 
    };

    public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {

            // delete baskedt from cache

            //delete basekte from db

            await basketRepository.DeleteBasket(request.UserName,cancellationToken);

            return new DeleteBasketResult(true);

        }
    }
}
