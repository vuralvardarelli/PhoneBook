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

        // Updating a record's contactInfo(add) on phonebook.
        [HttpPut("updateContactInfo")]
        public async Task<ActionResult> UpdateContactInfo()
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
        public async Task<ActionResult> GetRecord(string name, string surname, string guid = "")
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
