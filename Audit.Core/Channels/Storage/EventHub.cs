using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Audit.Core.Channels.Storage
{
    public class EventHub : IChannel
    {
        public Task<bool> WriteAsync(Models.Audit data)
        {
            throw new NotImplementedException();
        }
    }
}