using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Audit.Core.Channels.MessageBus
{
    public class EventHub : IChannel
    {

        private EventHubClient _eventHubClient;
        public EventHub(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            _eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
        }

        //process single
        public async Task WriteAsync<T>(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var serializedEvent = JsonConvert.SerializeObject(data);
            var eventBytes = Encoding.UTF8.GetBytes(serializedEvent);
            var eventData = new EventData(eventBytes);
            await _eventHubClient.SendAsync(eventData);
        }

        //process bulk
        public async Task WriteAsync<T>(IEnumerable<T> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var events = new List<EventData>();
            foreach (var myEvent in data)
            {
                var serializedEvent = JsonConvert.SerializeObject(myEvent);
                var eventBytes = Encoding.UTF8.GetBytes(serializedEvent);
                events.Add(new EventData(eventBytes));
            }
            await _eventHubClient.SendBatchAsync(events);
        }

    }
}