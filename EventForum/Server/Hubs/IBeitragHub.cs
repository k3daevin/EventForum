using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventForum.Server.Hubs
{
    public interface IBeitragHub
    {
        Task KommentarHinzugefuegt();
    }
}
