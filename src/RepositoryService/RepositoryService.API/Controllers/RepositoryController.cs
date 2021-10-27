using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepositoryService.Application.Commands;
using RepositoryService.Application.Responses;
using RepositoryService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RepositoryService.API.Controllers
{
    [Route("/repository")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RepositoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddRecord")]
        [ProducesResponseType(typeof(RecordResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> AddRecord([FromBody] AddRecordCommand request)
        {
            RecordResponse response = await _mediator.Send(request);

            if (response == null)
                return StatusCode(500, "Check Elasticsearch logs for more information");

            return Ok(response);
        }
    }
}
