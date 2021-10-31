using ReportService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Services.Interfaces
{
    public interface IReportService
    {
        Task<GenericResult> CreateReportRequest();
        Task<GenericResult> GetReports();
        Task<GenericResult> GetReport(int reportId);
        void UpdateReport(int reportId, List<Record> records);

    }
}
