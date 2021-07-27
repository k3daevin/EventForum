using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.ReadModels
{
    public class SimpleKommentarData
    {
        public Guid KommentarId { get; set; }
        public string Betreff { get; set; }
        public string Name { get; set; }
        public string Inhalt { get; set; }
        public DateTime Erstellt { get; set; }
    }
}
