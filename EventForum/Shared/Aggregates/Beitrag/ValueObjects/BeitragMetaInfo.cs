﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Aggregates.Beitrag.ValueObjects
{
    public class BeitragMetaInfo
    {
        public DateTime Erstellt { get; set; }
        public int GeloeschteKommentare { get; set; }
    }
}
