using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Models
{
    public class AppSettings
    {
        public string RepositoryServiceUrl { get; set; }
        public string ReportServiceUrl { get; set; }
        public string ElasticsearchUrl { get; set; }
        public string ElasticsearchIndex { get; set; }
    }
}
