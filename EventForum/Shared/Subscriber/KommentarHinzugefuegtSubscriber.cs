using EventFlow.Aggregates;
using EventFlow.Subscribers;
using EventForum.Shared.Aggregates.Beitrag;
using EventForum.Shared.Aggregates.Beitrag.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventForum.Shared.Subscriber
{
    public class KommentarHinzugefuegtSubscriber : ISubscribeAsynchronousTo<BeitragAggregate, BeitragId, KommentarHinzugefuegtEvent>
    {
        private readonly SubscriberEventContainer _subscriberEventContainer;

        public KommentarHinzugefuegtSubscriber(SubscriberEventContainer subscriberEventContainer)
        {
            _subscriberEventContainer = subscriberEventContainer;
        }

        public Task HandleAsync(IDomainEvent<BeitragAggregate, BeitragId, KommentarHinzugefuegtEvent> domainEvent, CancellationToken cancellationToken)
        {
            _subscriberEventContainer.Call(domainEvent.AggregateIdentity.Value);
            return Task.CompletedTask;
        }
    }
}
