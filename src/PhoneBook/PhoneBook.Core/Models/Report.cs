using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public int Status { get; set; } // "0" means "Not Ready", "1" means "Ready".
        public string Details { get; set; }
    }
}
