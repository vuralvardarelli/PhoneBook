using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Core.Entities
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
