using EventBusRabbitMQ.Models;
using System.Collections.Generic;

namespace EventBusRabbitMQ.Events
{
    public class SendRecordsEvent
    {
        public int RequestId { get; set; }
        public List<Record> Records { get; set; }
    }
}
