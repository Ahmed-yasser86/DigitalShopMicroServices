using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Abstractions;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandler.Domian
{
    public class CreateOrderEventHandler(ILogger<CreateOrderEventHandler> logger) :
        INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            throw new NotImplementedException();
        }
    }
}
