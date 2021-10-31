using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReportService.Core;
using ReportService.Core.Entities;
using ReportService.Core.Models;
using ReportService.Infrastructure.Data;
using ReportService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private readonly EventBusRabbitMQProducer _eventBus;

        public ReportService(ReportContext context, ILogger<ReportService> logger, ICacheService cacheService, AppSettings appSettings, EventBusRabbitMQProducer eventBus)
        {
            _context = context;
            _logger = logger;
            _cacheService = cacheService;
            _appSettings = appSettings;
            _eventBus = eventBus;
        }

        public async Task<GenericResult> CreateReportRequest()
        {
            GenericResult result = new GenericResult();

            try
            {
                Report report = new Report();
                report.RequestedDateTime = DateTime.Now;
                report.Status = 0;

                Monitor.Enter(_lock);
                _context.Reports.Add(report);
                _context.SaveChanges();
                report = _context.Reports.OrderByDescending(x => x.ReportId).FirstOrDefault();
                Monitor.Exit(_lock);

                if (report == null)
                    throw new ArgumentNullException("report");

                result.IsSucceeded = true;
                result.Data = report;

                _eventBus.PublishGetRecords(EventBusConstants.GetRecordsQueue, new EventBusRabbitMQ.Events.GetRecordsEvent()
                {
                    RequestId = report.ReportId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "ReportService", "CreateReportRequest", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = 500;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return await Task.FromResult(result);
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

        public void UpdateReport(int reportId, List<Record> records)
        {
            try
            {
                Monitor.Enter(_lock);
                Monitor.Exit(_lock);

                Report report = _context.Reports.FirstOrDefault(x => x.ReportId == reportId);

                if (report == null)
                    throw new ArgumentNullException("report");

                List<Record> recordsWithLocation = new List<Record>();

                foreach (var record in records)
                {
                    foreach (var contactInfo in record.ContactInfos)
                    {
                        if (contactInfo.Type == 2)
                            recordsWithLocation.Add(record);
                    }
                }

                List<ReportDetail> reportDetails = (from record in recordsWithLocation
                                                    from contactInfo in record.ContactInfos
                                                    where contactInfo.Type == 2
                                                    group record by contactInfo.Value into grp
                                                    select new ReportDetail
                                                    {
                                                        Location = grp.Key,
                                                        PeopleCountAtLocation = grp.Count()
                                                    }).ToList();

                report.Details = JsonConvert.SerializeObject(reportDetails);
                report.Status = 1;
                _context.Reports.Update(report);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "ReportService", "UpdateReport", _httpContextAccessor.HttpContext.TraceIdentifier);
            }
        }
    }
}
