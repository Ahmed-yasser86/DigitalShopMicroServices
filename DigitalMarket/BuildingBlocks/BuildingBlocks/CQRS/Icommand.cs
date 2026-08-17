using MediatR;

namespace BuildingBlocks.CQRS
{


    public interface Icommand : Icommand<Unit>
    {

    }
    public interface Icommand<out TResponse> : IRequest<TResponse>
    {
    }
}
