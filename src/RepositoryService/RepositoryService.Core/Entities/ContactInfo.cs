using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryService.Core.Entities
{
    public class ContactInfo
    {
        [Key]
        public int ContactInfoId { get; set; }

        public int Type { get; set; }
        public string Value { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public int? RecordRefId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("RecordRefId")]
        public Record Record { get; set; }
    }
}
