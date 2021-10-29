using RepositoryService.Core.Entities;
using RepositoryService.Core.Models;
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

        public async Task AddRecord(Record record)
        {
            await _context.AddAsync(record);
            await _context.SaveChangesAsync();
        }
    }
}
