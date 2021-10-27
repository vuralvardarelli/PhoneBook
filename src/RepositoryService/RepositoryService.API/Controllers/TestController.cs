using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryService.Core.Models;
using RepositoryService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryService.API.Controllers
{
    [Route("/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<TestController> _logger;

        public TestController(ICacheService cacheService, AppSettings appSettings, ILogger<TestController> logger)
        {
            _cacheService = cacheService;
            _appSettings = appSettings;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> RedisTest()
        {
            await _cacheService.Add("vural", _appSettings);

            AppSettings appS = await _cacheService.Get<AppSettings>("vural");

            _logger.LogInformation("TEST BRO");

            await _cacheService.Remove("vural");

            return Ok();
        }
    }
}
