using Microsoft.AspNetCore.Mvc;
using PhoneBook.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.Controllers
{
    [Route("/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;

        public ReportController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }



        //Requesting for a report
        [HttpGet]
        public void RequestReport()
        {
            _httpClientService.RequestReport();
        }

        // Request to get all reports with statuses
        [HttpGet("getAllReports")]
        public async Task<ActionResult> GetAllReports()
        {
            return Ok(await _httpClientService.GetAllReports());
        }

        // Request to get a single report with UUID
        [HttpGet("getReport")]
        public async Task<ActionResult> GetReport(int reportId)
        {
            return Ok(await _httpClientService.GetReport(reportId));
        }

        [HttpGet("getReportDetails")]
        public async Task<ActionResult> GetReportDetails(int reportId)
        {
            return Ok(await _httpClientService.GetReportDetails(reportId));
        }
    }
}
