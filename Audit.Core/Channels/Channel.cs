using System;
using System.Collections.Generic;
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

        public Task WriteAsync<T>(T data)
        {
            return _channel.WriteAsync(data);
        }

        public Task WriteAsync<T>(IEnumerable<T> data)
        {
            return _channel.WriteAsync(data);
        }
    }
}
