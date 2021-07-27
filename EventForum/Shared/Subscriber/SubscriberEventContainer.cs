using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Shared.Subscriber
{
    public class SubscriberEventContainer
    {
        public event Action<string> OnKommentarhinzugefuegt;
        internal void Call(string id) => OnKommentarhinzugefuegt?.Invoke(id);
    }
}
