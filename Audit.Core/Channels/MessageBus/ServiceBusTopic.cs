using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Audit.Core.Channels.MessageBus
{
    public class ServiceBusTopic : IChannel
    {
        public Task WriteAsync<T>(T data)
        {
            throw new NotImplementedException();
        }

        public Task WriteAsync<T>(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }
    }
}