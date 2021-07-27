using EventForum.Server.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventForum.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly BeitragHubContext _beitragHubContext;

        public SignalRController(BeitragHubContext beitragHubContext)
        {
            _beitragHubContext = beitragHubContext;
        }

        [HttpGet]
        public IEnumerable<string> Get() => _beitragHubContext.BeitragIds;
    }
}
