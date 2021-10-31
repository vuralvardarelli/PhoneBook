using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public static class EventBusConstants
    {
        public const string GetRecordsQueue = "getRecordsQueue";
        public const string SendRecordsQueue = "sendRecordsQueue";
    }
}
