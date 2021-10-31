using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepositoryService.Application.Commands;
using RepositoryService.Application.Queries;
using RepositoryService.Application.Responses;
using RepositoryService.Core.Entities;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddRecord([FromBody] AddRecordCommand request)
        {
            GenericResult result = await _mediator.Send(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        [HttpDelete("RemoveRecord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RemoveRecord([FromBody] RemoveRecordCommand request)
        {
            GenericResult result = await _mediator.Send(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        [HttpPost("AddContactInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddContactInfo([FromBody] AddContactInfoCommand request)
        {
            GenericResult result = await _mediator.Send(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        [HttpDelete("RemoveContactInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RemoveContactInfo([FromBody] RemoveContactInfoCommand request)
        {
            GenericResult result = await _mediator.Send(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        [HttpGet("GetRecords")]
        [ProducesResponseType(typeof(List<Record>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetRecords()
        {
            GenericResult result = await _mediator.Send(new GetRecordsQuery());

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }

        [HttpGet("GetRecord")]
        [ProducesResponseType(typeof(Record), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetRecord(int recordId)
        {
            GenericResult result = await _mediator.Send(new GetRecordQuery()
            {
                RecordId = recordId
            });

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }
    }
}
