using Microsoft.EntityFrameworkCore;
using ReportService.Core.Entities;

namespace ReportService.Infrastructure.Data
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {

        }

        public DbSet<Report> Reports { get; set; }
    }
}
