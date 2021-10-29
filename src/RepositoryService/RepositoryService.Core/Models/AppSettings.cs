using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Core.Models
{
    public class AppSettings
    {
        public string RedisUrl { get; set; }
        public string ElasticsearchUrl { get; set; }
        public string ElasticsearchIndex { get; set; }
        public string RecordsCacheKey { get; set; }
        public int RecordsCacheTimeoutAsSeconds { get; set; }
    }
}
