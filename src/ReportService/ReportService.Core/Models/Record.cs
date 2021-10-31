using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReportService.Core.Models
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
