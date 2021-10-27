using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Core.Entities
{
    public class ContactInfo
    {
        [Key]
        public string UUID { get; set; } = Guid.NewGuid().ToString();

        public int Type { get; set; }
        public string Value { get; set; }

        public string RecordForeignKey { get; set; }
        public Record Record { get; set; }
    }
}
