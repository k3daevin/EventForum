using EventForum.Shared.Subscriber;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventForum.Server.Hubs
{
    public class BeitragHub : Hub<IBeitragHub>
    {
        public async Task JoinBeitragGroup(string beitragId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, beitragId);
        }
        public async Task LeaveBeitragGroup(string beitragId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, beitragId);
        }
    }
}
