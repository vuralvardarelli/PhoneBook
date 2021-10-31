using System;
using System.ComponentModel.DataAnnotations;

namespace ReportService.Core.Entities
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public int Status { get; set; } // "0" means "Not Ready", "1" means "Ready".
        public string Details { get; set; }
    }
}
