
using BuildingBlocks.CQRS;
using catalog.API.Models;
using catalog.API.Products.UpdateProduct;
using FluentValidation;
using Marten;

namespace catalog.API.Products.CreateProduct
{


    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : Icommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);


    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }

        //}
        public class CreateProductHandler(IDocumentSession Session) : ICommandHandler<CreateProductCommand, CreateProductResult>
        {
            public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {

                //var validatorResult = await validator.ValidateAsync(request,cancellationToken);
                //var errors = validatorResult.Errors.Select(x =>x.ErrorMessage).ToList();

                //if (errors.Any())
                //{
                //    throw new ValidationException(errors.FirstOrDefault());
                //}

                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Category = request.Category,
                    Description = request.Description,
                    ImageFile = request.ImageFile,
                    Price = request.Price
                };

                Session.Store(product);
                await Session.SaveChangesAsync();
                return new CreateProductResult(product.Id);

            }


        }
    }
}
