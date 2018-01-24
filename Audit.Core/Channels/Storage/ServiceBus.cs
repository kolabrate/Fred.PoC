using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;


namespace Audit.Core.Channels.Storage
{
    public class ServiceBus : IChannel
    {
        private string _serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBus"];
        private TopicClient Topic { get; set; }

        //check if queue exists or create a queue
        public ServiceBus(string serviceBusConnectionString)
        {


            
        }

        public Task<bool> WriteAsync(Models.Audit data)
        {
            throw new NotImplementedException();
        }

        

    }
}