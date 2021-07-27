using EventFlow.Core;
using EventFlow.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class BeitragId : Identity<BeitragId>
    {
        public BeitragId(string value) : base(value)
        {
        }
    }
}
