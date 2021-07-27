using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.Commands
{
    public class FuegeKommentarHinzuCommand : Command<BeitragAggregate, BeitragId, IExecutionResult>
    {
        public FuegeKommentarHinzuCommand(BeitragId aggregateId, KommentarData kommentarData) : base(aggregateId)
        {
            KommentarData = kommentarData;
        }

        public KommentarData KommentarData { get; set; }
    }
}
