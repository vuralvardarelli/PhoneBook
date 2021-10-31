using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events
{
    public class GetRecordsEvent
    {
        public int RequestId { get; set; }
    }
}
