﻿using System;
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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public List<ContactInfo> ContactInfos { get; set; }
    }
}
