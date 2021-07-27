using EventFlow.Queries;
using EventForum.Shared.Aggregates.Beitrag.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.Queries
{
    public class VorschauQuery : IQuery<IEnumerable<VorschauReadModel>>
    {
        public int Begin { get; set; }
        public int Size { get; set; }
    }
}
