using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Data.Interfaces
{
    public interface ICacheContext
    {
        IDatabase Redis { get; }
    }
}
