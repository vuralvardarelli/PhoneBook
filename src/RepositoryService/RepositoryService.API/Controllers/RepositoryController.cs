using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepositoryService.Application.Commands;
using RepositoryService.Application.Responses;
using RepositoryService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult> Test()
        {
            AddRecordCommand arc = new AddRecordCommand()
            {
                Company = "Microsoft",
                Name = "John",
                Surname = "Doe",
                ContactInfos = new List<Core.Entities.ContactInfo>()
                {
                    new Core.Entities.ContactInfo()
                    {
                        Type = (int)ContactInfoType.Email,
                        Value = "johndoe@microsoft.com"
                    }
                }
            };

            RecordResponse resp = await _mediator.Send(arc);

            if (resp == null)
                return StatusCode(500, "Check Elasticsearch logs for more information");

            return Ok(resp);
        }
    }
}
