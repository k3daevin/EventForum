using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.ValueObjects
{
    public class BeitragData
    {
        public BeitragMetaInfo MetaInfo { get; set; }
        public List<KommentarData> Kommentare { get; set; } = new List<KommentarData>();
    }
}
