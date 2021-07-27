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
    public class ErstelleBeitragCommandHandler :
        CommandHandler<BeitragAggregate, BeitragId, IExecutionResult, ErstelleBeitragCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BeitragAggregate aggregate, ErstelleBeitragCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.ErstelleBeitrag(command.KommentarData));
        }
    }
}
