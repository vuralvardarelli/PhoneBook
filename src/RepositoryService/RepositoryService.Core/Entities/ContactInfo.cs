using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Core.Entities
{
    public class ContactInfo
    {
        [Key]
        public int ContactInfoId { get; set; }

        public int Type { get; set; }
        public string Value { get; set; }

        public int? RecordRefId { get; set; }

        [ForeignKey("RecordRefId")]
        public Record Record { get; set; }
    }
}
