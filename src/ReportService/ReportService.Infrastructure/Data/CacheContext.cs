using ReportService.Core.Models;
using ReportService.Infrastructure.Data.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Data
{
    public class CacheContext : ICacheContext
    {
        private ConnectionMultiplexer _multiplexer;

        private AppSettings _settings;

        public CacheContext(AppSettings settings)
        {
            _settings = settings;
            _multiplexer = ConnectionMultiplexer.Connect(_settings.RedisUrl);
            Redis = _multiplexer.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
