using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Audit.Core.Channels.MessageBus
{
    public class ServiceBusQueue : IChannel
    {
        private string _serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBus"];
        private QueueClient Queue { get; set; }
        public ServiceBusQueue(string serviceBusConnectionString)
        {


            
        }

        public Task<bool> WriteAsync(Models.Audit data)
        {
            throw new NotImplementedException();
        }

        

    }
}