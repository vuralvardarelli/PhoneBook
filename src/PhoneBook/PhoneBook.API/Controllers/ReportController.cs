using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.Models;
using PhoneBook.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RequestReport()
        {
            GenericResult result = await _httpClientService.RequestReport();

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        // Request to get all reports with statuses
        [HttpGet("getAllReports")]
        [ProducesResponseType(typeof(List<Report>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetAllReports()
        {
            GenericResult result = await _httpClientService.GetAllReports();

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }

        // Request to get a single report with id
        [HttpGet("getReport")]
        [ProducesResponseType(typeof(Report), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetReport(int reportId)
        {
            GenericResult result = await _httpClientService.GetReport(reportId);

                if(!result.IsSucceeded)
                    return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }

        [HttpGet("getReportDetails")]
        [ProducesResponseType(typeof(List<ReportDetail>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetReportDetails(int reportId)
        {
            GenericResult result = await _httpClientService.GetReportDetails(reportId);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }
    }
}
