using Microsoft.AspNetCore.Mvc;
using ReportService.Core.Models;
using ReportService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReportService.API.Controllers
{
    [Route("/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("RequestReport")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RequestReport()
        {
            GenericResult result = await _reportService.CreateReportRequest();

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result);
        }

        [HttpGet("ListReports")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> ListReports()
        {
            GenericResult result = await _reportService.GetReports();

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetReport(int reportId)
        {
            GenericResult result = await _reportService.GetReport(reportId);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result);
        }
    }
}
