using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.ValueObjects
{
    public class KommentarMetaInfo
    {
        public Guid KommentarId { get; set; }
        public string UserId { get; set; }
        public DateTime LoeschDatum { get; set; }      
    }
}
