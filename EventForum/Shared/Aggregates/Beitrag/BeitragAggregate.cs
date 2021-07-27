using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventForum.Shared.Aggregates.Beitrag.Events;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag
{
    public class BeitragAggregate : AggregateRoot<BeitragAggregate, BeitragId>,
        IEmit<BeitragErstelltEvent>,
        IEmit<KommentarHinzugefuegtEvent>
    {
        public BeitragAggregate(BeitragId id) : base(id)
        {
        }

        public BeitragData Beitrag { get; set; } = new BeitragData();

        public void Apply(BeitragErstelltEvent aggregateEvent)
        {
            Beitrag.MetaInfo = aggregateEvent.BeitragMetaInfo;
            AddKommentar(aggregateEvent.Kommentar);
        }

        public void Apply(KommentarHinzugefuegtEvent aggregateEvent)
        {
            AddKommentar(aggregateEvent.Kommentar);
        }

        private void AddKommentar(KommentarData kommentarData)
        {
            kommentarData.MetaInfo.KommentarId = Guid.NewGuid();
            Beitrag.Kommentare.Add(kommentarData);
        }


        internal IExecutionResult ErstelleBeitrag(KommentarData kommentarData)
        {
            if (IsNew)
            {
                Emit(new BeitragErstelltEvent
                {
                    BeitragMetaInfo = new BeitragMetaInfo
                    {
                        Erstellt = DateTime.UtcNow,
                        GeloeschteKommentare = 0,
                    },
                    Kommentar = kommentarData,
                });
                return ExecutionResult.Success();
            } else
            {
                return ExecutionResult.Failed("Beitrag wurde schon angelegt");
            }
        }

        internal IExecutionResult FuegeKommentarHinzu(KommentarData kommentarData)
        {
            if (IsNew)
            {                
                return ExecutionResult.Failed("Beitrag wurde noch nicht angelegt");
            }
            else
            {
                Emit(new KommentarHinzugefuegtEvent
                {
                    Kommentar = kommentarData,
                });
                return ExecutionResult.Success();
            }
        }
    }
}
