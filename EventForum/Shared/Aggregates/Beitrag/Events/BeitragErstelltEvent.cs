using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.Events
{
    [EventVersion("beitragerstellt", 1)]
    public class BeitragErstelltEvent : AggregateEvent<BeitragAggregate, BeitragId>
    {
        public BeitragMetaInfo BeitragMetaInfo { get; set; }
        public KommentarData Kommentar { get; set; }
    }
}
