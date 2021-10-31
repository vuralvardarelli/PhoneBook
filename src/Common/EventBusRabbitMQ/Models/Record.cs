using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Models
{
    public class Record
    {
        public int RecordId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public List<ContactInfo> ContactInfos { get; set; }
    }
}
