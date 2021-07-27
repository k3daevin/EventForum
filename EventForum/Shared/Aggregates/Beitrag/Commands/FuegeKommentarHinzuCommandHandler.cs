using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.Commands
{
    
    public class FuegeKommentarHinzuCommandHandler :
    CommandHandler<BeitragAggregate, BeitragId, IExecutionResult, FuegeKommentarHinzuCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BeitragAggregate aggregate, FuegeKommentarHinzuCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.FuegeKommentarHinzu(command.KommentarData));
        }
    }
}
