using BuildingBlocks.CQRS;
using MediatR;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : Icommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var context = new ValidationContext<TRequest>(request);
            var validataionResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context,cancellationToken)));

            var failures = validataionResults
                .Where(r =>r.Errors.Any()).
                SelectMany(e =>e.Errors).ToList();


            if (failures.Any())
            {
                throw new FluentValidation.ValidationException(failures);
            }

            return await next();

        }
    }
}
