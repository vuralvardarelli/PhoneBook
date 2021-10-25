using Microsoft.AspNetCore.Mvc;
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
        //Requesting for a report
        [HttpGet]
        public async Task<ActionResult> RequestReport()
        {
            return Ok();
        }

        // Request to get all reports with statuses
        [HttpGet("getAllReports")]
        public async Task<ActionResult> GetAllReports()
        {
            return Ok();
        }

        // Request to get a single report with UUID
        [HttpGet("getReport")]
        public async Task<ActionResult> GetReports(string guid)
        {
            return Ok();
        }
    }
}
