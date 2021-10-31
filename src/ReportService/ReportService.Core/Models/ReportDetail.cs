using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Models
{
    public class ReportDetail
    {
        public string Location { get; set; }
        public int PeopleCountAtLocation { get; set; }
        public int PhoneNumberCountAtLocation { get; set; }
    }
}
