using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventForum.Shared.Aggregates.Beitrag;
using EventForum.Shared.Aggregates.Beitrag.Commands;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventForum.Shared.CommandModels;
using EventForum.Server.Hubs;

namespace EventForum.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly BeitragHubContext _beitragHubContext;

        public CommandController(ICommandBus commandBus, BeitragHubContext beitragHubContext)
        {
            
            _commandBus = commandBus;
            _beitragHubContext = beitragHubContext;
        }

        [HttpPost("beitrag")]
        public async Task<IActionResult> NeuerBeitrag([FromBody]KommentarData kommentarData, CancellationToken cancellationToken)
        {
            var newId = BeitragId.New;
            kommentarData.Erstellt = DateTime.Now;
            var command = new ErstelleBeitragCommand(newId, kommentarData);
            var result = ToExecutionResultResponse(await _commandBus.PublishAsync(command, cancellationToken));
            if (result.IsSuccess)
            {
                return Ok(new ResponseWithBeitragId{ Response = result, BeitragId = newId.Value, });
            } else
            {
                return BadRequest(new { result });
            }
        }

        [HttpPost("beitrag/{beitragId}")]
        public async Task<ExecutionResultResponse> KommentarHinzufuegen([FromBody] KommentarData kommentarData, string beitragId, CancellationToken cancellationToken)
        {
            kommentarData.Erstellt = DateTime.Now;
            var command = new FuegeKommentarHinzuCommand(new BeitragId(beitragId), kommentarData);
            var result = await _commandBus.PublishAsync(command, cancellationToken);
            return ToExecutionResultResponse(result);
        }


        private static ExecutionResultResponse ToExecutionResultResponse(IExecutionResult executionResult)
        {
            if (executionResult is SuccessExecutionResult)
            {
                return new ExecutionResultResponse() { IsSuccess = true };
            }
            else
            {
                return new ExecutionResultResponse()
                {
                    IsSuccess = false,
                    Errors = (executionResult as FailedExecutionResult)?.Errors?.ToList(),
                };
            }
        }
    }
}
