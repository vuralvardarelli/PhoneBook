using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Models
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
    }
}
