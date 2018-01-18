using System;
using System.Threading.Tasks;

namespace Audit.Core.Channels
{
    public class Channel : IChannel
    {
        private readonly IChannel _channel;

        public Channel(IChannel channel)
        {
            this._channel = channel;
        }

        public Task<bool> WriteAsync(Models.Audit data)
        {
            return _channel.WriteAsync(data);
        }

    }
}
