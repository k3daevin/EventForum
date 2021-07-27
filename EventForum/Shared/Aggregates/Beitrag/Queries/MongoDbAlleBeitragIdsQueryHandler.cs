using EventFlow.MongoDB.ReadStores;
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
    public class MongoDbAlleBeitragIdsQueryHandler : IQueryHandler<AlleBeitraegeQuery, IEnumerable<string>>
    {
        private readonly IMongoDbReadModelStore<BeitragReadModel> _mongoDbReadStore;

        public MongoDbAlleBeitragIdsQueryHandler(IMongoDbReadModelStore<BeitragReadModel> mongoDbReadStore)
        {
            _mongoDbReadStore = mongoDbReadStore;
        }
        public async Task<IEnumerable<string>> ExecuteQueryAsync(AlleBeitraegeQuery query, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            var cursor = await _mongoDbReadStore.FindAsync(x => true, cancellationToken: cancellationToken);
            while(await cursor.MoveNextAsync())
            {
                list.AddRange(cursor.Current.Select(b => b.Id));
            }
            return list;
        }
    }
}
