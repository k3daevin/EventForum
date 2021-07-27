using EventForum.Shared.Subscriber;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventForum.Server.Hubs
{
    public class BeitragHubContext
    {
        private IHubContext<BeitragHub, IBeitragHub> _hubContext;
        private readonly SubscriberEventContainer _subscriberEventContainer;

        private readonly List<string> _listBeitragIds = new List<string>();

        public BeitragHubContext(IHubContext<BeitragHub, IBeitragHub> hubContext, SubscriberEventContainer subscriberEventContainer)
        {
            _hubContext = hubContext;
            _subscriberEventContainer = subscriberEventContainer;
            _subscriberEventContainer.OnKommentarhinzugefuegt += KommentarHinzugefuegt;

        }

        public IEnumerable<string> BeitragIds => _listBeitragIds;

        public void KommentarHinzugefuegt(string beitragId)
        {
            _listBeitragIds.Add(beitragId);
            _hubContext.Clients.All.KommentarHinzugefuegt().Wait();
            //_hubContext.Clients.Group(beitragId).KommentarHinzugefuegt().Wait();
        }

    }
}
