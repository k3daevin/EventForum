using AutoMapper;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using EventForum.Shared.Aggregates.Beitrag.Events;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.ReadModels
{
    public class VorschauReadModel : 
        IReadModel,
        IMongoDbReadModel,
        IAmReadModelFor<BeitragAggregate, BeitragId, BeitragErstelltEvent>,
        IAmReadModelFor<BeitragAggregate, BeitragId, KommentarHinzugefuegtEvent>
    {
        public SimpleBeitragData Beitrag { get; set; }
        public DateTime LetzteAenderung { get; set; }

        private static IMapper Mapper { get; set; }

        public string Id { get; set; }

        public long? Version { get; set; }

        static VorschauReadModel()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<KommentarData, SimpleKommentarData>());
            Mapper = config.CreateMapper();
        }

        public void Apply(IReadModelContext context, IDomainEvent<BeitragAggregate, BeitragId, BeitragErstelltEvent> domainEvent)
        {
            Id = context.ReadModelId;
            Beitrag = new SimpleBeitragData()
            {
                BeitragId = domainEvent.AggregateIdentity.Value,
                Erstellt = domainEvent.AggregateEvent.BeitragMetaInfo.Erstellt,
                Kommentare = new List<SimpleKommentarData>(),
            };
            AddKommentar(domainEvent.AggregateEvent.Kommentar);
            UpdateLetzteAenderung(domainEvent);
        }

        public void Apply(IReadModelContext context, IDomainEvent<BeitragAggregate, BeitragId, KommentarHinzugefuegtEvent> domainEvent)
        {
            AddKommentar(domainEvent.AggregateEvent.Kommentar);
            UpdateLetzteAenderung(domainEvent);
            while (Beitrag.Kommentare.Count > 4)
            {
                Beitrag.Kommentare.RemoveAt(0);
            }
        }



        private void AddKommentar(KommentarData kommentar)
        {
            var simpleKommentar = Mapper.Map<SimpleKommentarData>(kommentar);
            Beitrag.Kommentare.Add(simpleKommentar);
        }

        private void UpdateLetzteAenderung(IDomainEvent domainEvent)
        {
            LetzteAenderung = domainEvent.Timestamp.DateTime;
        }
    }
}
