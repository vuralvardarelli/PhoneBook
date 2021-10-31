using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportService.Core;
using ReportService.Core.Entities;
using ReportService.Core.Models;
using ReportService.Infrastructure.Data;
using ReportService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportContext _context;
        private readonly ILogger<ReportService> _logger;
        private readonly ICacheService _cacheService;
        private readonly AppSettings _appSettings;
        private static object _lock = new object();
        private static IHttpContextAccessor _httpContextAccessor;

        public ReportService(ReportContext context, ILogger<ReportService> logger, ICacheService cacheService, AppSettings appSettings)
        {
            _context = context;
            _logger = logger;
            _cacheService = cacheService;
            _appSettings = appSettings;
        }

        public async Task<GenericResult> CreateReportRequest()
        {
            GenericResult result = new GenericResult();

            try
            {
                Report report = new Report();
                report.RequestedDateTime = DateTime.Now;
                report.Status = 0;

                await _context.Reports.AddAsync(report);
                await _context.SaveChangesAsync();
                report = _context.Reports.OrderByDescending(x => x.ReportId).FirstOrDefault();

                if (report == null)
                    throw new ArgumentNullException("report");

                result.IsSucceeded = true;
                result.Data = report;

                // TODO: Send Request to RepositoryService for all information.
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "ReportService", "CreateReportRequest", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = 500;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> GetReport(int reportId)
        {
            GenericResult result = new GenericResult();

            try
            {
                Report report = await _context.Reports.FirstOrDefaultAsync(x => x.ReportId == reportId);

                if (report == null)
                    throw new ArgumentNullException("report");

                result.Data = report;
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "ReportService", "GetReport", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = 500;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> GetReports()
        {
            GenericResult result = new GenericResult();

            try
            {
                List<Report> reports = null;

                if (await _cacheService.Any(_appSettings.ReportsCacheKey))
                {
                    reports = await _cacheService.Get<List<Report>>(_appSettings.ReportsCacheKey);
                }
                else
                {
                    reports = await _context.Reports.ToListAsync();

                    await _cacheService.AddWithExpire(_appSettings.ReportsCacheKey, reports, _appSettings.ReportsCacheTimeoutAsSeconds);
                }

                result.Data = reports;
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "ReportService", "GetReports", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = 500;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }
    }
}
