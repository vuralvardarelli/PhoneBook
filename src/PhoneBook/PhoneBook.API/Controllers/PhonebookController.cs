using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.Models;
using PhoneBook.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhoneBook.API.Controllers
{
    [Route("/phonebook")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;

        public PhonebookController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        // Creating a record on phonebook.
        [HttpPost("createRecord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateRecord([FromBody]AddRecordCommand request)
        {
            GenericResult result = await _httpClientService.CreateRecord(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        // Deleting a record on phonebook.
        [HttpDelete("deleteRecord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteRecord([FromBody]RemoveRecordCommand request)
        {
            GenericResult result = await _httpClientService.DeleteRecord(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        // Add a contactInfo to a record on phonebook.
        [HttpPost("addContactInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddContactInfo([FromBody]AddContactInfoCommand request)
        {
            GenericResult result = await _httpClientService.AddContactInfo(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        // Deleting a record's contactInfo(delete) on phonebook.
        [HttpDelete("deleteContactInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteContactInfo([FromBody]RemoveContactInfoCommand request)
        {
            GenericResult result = await _httpClientService.DeleteContactInfo(request);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok();
        }

        // Get a record on phonebook with contactInfo
        [HttpGet("getRecord")]
        [ProducesResponseType(typeof(Record), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetRecord(int recordId)
        {
            GenericResult result = await _httpClientService.GetRecord(recordId);

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }

        // Get all records on phonebook with contactInfo
        [HttpGet("getRecords")]
        [ProducesResponseType(typeof(List<Record>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetRecords()
        {
            GenericResult result = await _httpClientService.GetRecords();

            if (!result.IsSucceeded)
                return StatusCode(result.StatusCode, $"Check Elasticsearch logs for more information : {result.Message}");

            return Ok(result.Data);
        }
    }
}
