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
        public string UUID { get; set; } = Guid.NewGuid().ToString();

        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }
}
