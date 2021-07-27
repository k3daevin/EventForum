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
    public class ErstelleBeitragCommand : Command<BeitragAggregate, BeitragId, IExecutionResult>
    {
        public ErstelleBeitragCommand(BeitragId aggregateId, KommentarData kommentarData) : base(aggregateId)
        {
            KommentarData = kommentarData;
        }

        public KommentarData KommentarData { get; set; }
    }
}
