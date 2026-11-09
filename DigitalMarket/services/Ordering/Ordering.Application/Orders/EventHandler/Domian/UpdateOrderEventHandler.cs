using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandler.Domian
{
    internal class UpdateOrderEventHandler(ILogger<UpdateOrderEventHandler> logger) :
        INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            throw new NotImplementedException();
        }
    }
}
