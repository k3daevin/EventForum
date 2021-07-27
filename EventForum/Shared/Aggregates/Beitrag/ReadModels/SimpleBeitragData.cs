using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.ReadModels
{
    public class SimpleBeitragData
    {
        public string BeitragId { get; set; }
        public DateTime Erstellt { get; set; }
        public List<SimpleKommentarData> Kommentare { get; set; }
    }
}
