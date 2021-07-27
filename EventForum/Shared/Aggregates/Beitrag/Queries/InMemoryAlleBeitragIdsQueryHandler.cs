using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using EventForum.Shared.Aggregates.Beitrag.ReadModels;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.Queries
{
    public class InMemoryAlleBeitragIdsQueryHandler : IQueryHandler<AlleBeitraegeQuery, IEnumerable<string>>
    {
        private readonly IInMemoryReadStore<BeitragReadModel> _inMemoryReadStore;

        public InMemoryAlleBeitragIdsQueryHandler(IInMemoryReadStore<BeitragReadModel> inMemoryReadStore)
        {
            _inMemoryReadStore = inMemoryReadStore;
        }
        public Task<IEnumerable<string>> ExecuteQueryAsync(AlleBeitraegeQuery query, CancellationToken cancellationToken)
        {
            return _inMemoryReadStore.FindAsync(x => true, cancellationToken)
                .ContinueWith(t => t.Result.Select(b => b.Beitrag.BeitragId));
        }
    }
}
