using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace BuildingBlocks.CQRS
{

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit> where TCommand : Icommand
    {

    }
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : Icommand<TResponse>
    {
  
    }
}
