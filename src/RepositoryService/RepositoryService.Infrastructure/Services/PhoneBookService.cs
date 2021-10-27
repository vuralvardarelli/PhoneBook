using RepositoryService.Core.Entities;
using RepositoryService.Infrastructure.Data;
using RepositoryService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryService.Infrastructure.Services
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly PhonebookContext _context;
        private static object _lock = new object();

        public PhoneBookService(PhonebookContext context)
        {
            _context = context;
        }

        public Record AddRecord(Record record)
        {
            Monitor.Enter(_lock);
            _context.Add(record);
            _context.SaveChanges();
            Record recordResp = _context.Records.OrderByDescending(x => x.RecordId).FirstOrDefault();
            Monitor.Exit(_lock);

            return recordResp;
        }
    }
}
