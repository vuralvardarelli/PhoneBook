using EventBusRabbitMQ.Models;
using System.Collections.Generic;

namespace EventBusRabbitMQ.Events
{
    public class SendRecordsEvent
    {
        public List<Record> Records { get; set; }
    }
}
