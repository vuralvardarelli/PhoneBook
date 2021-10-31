using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.API.Controllers
{
    [Route("/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("RequestReport")]
        public async Task<ActionResult> RequestReport()
        {
            return Ok();
        }

        [HttpGet("ListReports")]
        public async Task<ActionResult> ListReports()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetReport(int reportId)
        {
            return Ok();
        }
    }
}
