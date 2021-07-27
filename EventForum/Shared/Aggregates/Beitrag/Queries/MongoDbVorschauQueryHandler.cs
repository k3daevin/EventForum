using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using EventForum.Shared.Aggregates.Beitrag.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace EventForum.Shared.Aggregates.Beitrag.Queries
{
    public class MongoDbVorschauQueryHandler : IQueryHandler<VorschauQuery, IEnumerable<VorschauReadModel>>
    {
        private readonly IMongoDbReadModelStore<VorschauReadModel> _mongoDbReadStore;
        private readonly SortDefinition<VorschauReadModel> _sortDefinition;

        public MongoDbVorschauQueryHandler(IMongoDbReadModelStore<VorschauReadModel> mongoDbReadStore)
        {
            _mongoDbReadStore = mongoDbReadStore;
            _sortDefinition = (new SortDefinitionBuilder<VorschauReadModel>()).Descending(vorschau => vorschau.LetzteAenderung);

        }
        public async Task<IEnumerable<VorschauReadModel>> ExecuteQueryAsync(VorschauQuery query, CancellationToken cancellationToken)
        {
            var list = new List<VorschauReadModel>();
            var findOptions = new FindOptions<VorschauReadModel, VorschauReadModel>()
            {
                Sort = _sortDefinition,
                Skip = query.Begin,
                Limit = query.Size,
            };

            var cursor = await _mongoDbReadStore.FindAsync(x => true, findOptions, cancellationToken);
            while(await cursor.MoveNextAsync())
            {
                list.AddRange(cursor.Current);
            }
            return list;
        }
    }
}
