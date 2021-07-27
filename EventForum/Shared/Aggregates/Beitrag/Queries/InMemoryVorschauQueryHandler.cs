using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using EventForum.Shared.Aggregates.Beitrag.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.Queries
{
    public class InMemoryVorschauQueryHandler : IQueryHandler<VorschauQuery, IEnumerable<VorschauReadModel>>
    {
        private readonly IInMemoryReadStore<VorschauReadModel> _inMemoryReadStore;

        public InMemoryVorschauQueryHandler(IInMemoryReadStore<VorschauReadModel> inMemoryReadStore)
        {
            _inMemoryReadStore = inMemoryReadStore;
        }
        public Task<IEnumerable<VorschauReadModel>> ExecuteQueryAsync(VorschauQuery query, CancellationToken cancellationToken)
        {
            return _inMemoryReadStore.FindAsync(x => true, cancellationToken)
                .ContinueWith(t =>
                {
                    return t.Result
                    .OrderByDescending(vorschauReadModel => vorschauReadModel.LetzteAenderung)
                    .Skip(query.Begin)
                    .Take(query.Size);
                });
        }
    }
}
