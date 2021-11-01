using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.Controllers
{
    [Route("/phonebook")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        // Creating a record on phonebook.
        [HttpPost("createRecord")]
        public async Task<ActionResult> CreateRecord()
        {
            return Ok();
        }

        // Deleting a record on phonebook.
        [HttpDelete("deleteRecord")]
        public async Task<ActionResult> DeleteRecord()
        {
            return Ok();
        }

        // Add a contactInfo to a record on phonebook.
        [HttpPut("addContactInfo")]
        public async Task<ActionResult> AddContactInfo()
        {
            return Ok();
        }

        // Deleting a record's contactInfo(delete) on phonebook.
        [HttpDelete("deleteContactInfo")]
        public async Task<ActionResult> DeleteContactInfo()
        {
            return Ok();
        }

        // Get a record on phonebook with contactInfo
        [HttpGet("getRecord")]
        public async Task<ActionResult> GetRecord(int recordId)
        {
            return Ok();
        }

        // Get all records on phonebook with contactInfo
        [HttpGet("getRecords")]
        public async Task<ActionResult> GetRecords()
        {
            return Ok();
        }
    }
}
