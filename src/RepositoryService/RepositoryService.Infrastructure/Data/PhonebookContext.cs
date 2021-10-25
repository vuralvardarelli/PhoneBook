using Microsoft.EntityFrameworkCore;
using RepositoryService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Infrastructure.Data
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options)
        {

        }

        public DbSet<Record> Records { get; set; }
    }
}
