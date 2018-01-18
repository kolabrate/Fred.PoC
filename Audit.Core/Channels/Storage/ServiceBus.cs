using System;
using System.Threading.Tasks;


namespace Audit.Core.Channels.Storage
{
    public class ServiceBus : IChannel
    {
        public Task<bool> WriteAsync(Models.Audit data)
        {
            throw new NotImplementedException();

        }

    }
}