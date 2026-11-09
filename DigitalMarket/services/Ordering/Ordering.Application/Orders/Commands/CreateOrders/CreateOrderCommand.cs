using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrders
{
   public record  CreateOrderCommand(OrderDto orderDto) : Icommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id) ;
    

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand> {
    
    public CreateOrderCommandValidator()
        {
            RuleFor(x=> x.orderDto.OrderItems).NotEmpty().WithMessage("Order Items Could Not Be Empty");
            RuleFor(x=>x.orderDto.CustomerId).NotEmpty().WithMessage("Customer Id Could Not Be Empty");
            RuleFor(x=>x.orderDto.OrderName).NotEmpty().WithMessage("Order Name Is Required")
        }

    
    
    }

}
