using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.InfraStructure.Data.Intercepteors
{
    public class DispatchDomainEventsInterceptors(IMediator mediator) : SaveChangesInterceptor
    {


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {

            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        public async Task DispatchDomainEvents(DbContext? dbContext)
        {

            var Aggregates = dbContext.ChangeTracker
                .Entries<IAggregate>().Where
                (a => a.Entity.DomainEvents.Any()).
                Select(a =>a.Entity);


            var DomainEvents = Aggregates.
                SelectMany(a => a.DomainEvents).
                ToList();

            Aggregates.ToList().ForEach(a => a.ClearDomainEvent());

            foreach(var domainEvent in DomainEvents)
            {
                await mediator.Publish(domainEvent);
            }

        }

    }
}
