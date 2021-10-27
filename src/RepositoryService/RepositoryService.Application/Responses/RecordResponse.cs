using RepositoryService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Responses
{
    public class RecordResponse
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public List<ContactInfoResponse> ContactInfos { get; set; }
    }
}
