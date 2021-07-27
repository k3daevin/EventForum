using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventForum.Shared.CommandModels
{
    public class ResponseWithBeitragId
    {
        public ExecutionResultResponse Response { get; set; }
        public string BeitragId { get; set; }
    }
}
