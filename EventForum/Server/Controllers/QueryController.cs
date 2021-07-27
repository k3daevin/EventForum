using EventFlow.Queries;
using EventForum.Shared.Aggregates.Beitrag.Queries;
using EventForum.Shared.Aggregates.Beitrag.ReadModels;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventForum.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;

        public QueryController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        [HttpGet("all")]
        public Task<IEnumerable<string>> GetAll(CancellationToken cancellationToken)
        {
            return _queryProcessor.ProcessAsync(new AlleBeitraegeQuery(), cancellationToken);
        }

        [HttpGet("{beitragId}/{cacheHack}")]
        public Task<BeitragReadModel> GetBeitrag(string beitragId, string cacheHack, CancellationToken cancellationToken)
        {
            return _queryProcessor.ProcessAsync(new ReadModelByIdQuery<BeitragReadModel>(beitragId), cancellationToken);
        }

        [HttpGet("vorschau")]
        public Task<IEnumerable<VorschauReadModel>> GetVorschau([FromQuery] int begin, [FromQuery] int size, CancellationToken cancellationToken)
        {
            return _queryProcessor.ProcessAsync(new VorschauQuery { Begin = begin, Size = size }, cancellationToken);
        }
    }
}
