using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Core.Models
{
    public class GenericResult
    {
        public bool IsSucceeded { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } = new object();
    }
}
