using System;
using System.Threading.Tasks;

namespace Audit.Core.Channels.MessageBus
{
    public class EventHub : IChannel
    {
        public Task<bool> WriteAsync(Models.Audit data)
        {
            throw new NotImplementedException();
        }
    }
}