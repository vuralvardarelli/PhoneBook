using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RepositoryService.Application.Queries;
using RepositoryService.Core;
using RepositoryService.Core.Models;
using RepositoryService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryService.Application.Handlers
{
    public class GetRecordsHandler : IRequestHandler<GetRecordsQuery, GenericResult>
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<GetRecordsHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetRecordsHandler(IPhoneBookService phoneBookService, ILogger<GetRecordsHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _phoneBookService = phoneBookService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResult> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            GenericResult resp = new GenericResult();

            try
            {
                resp.Data = await _phoneBookService.GetRecords();
                resp.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "GetRecordsHandler", "Handle", _httpContextAccessor.HttpContext.TraceIdentifier);
                resp.StatusCode = 500;
                resp.Message = ex.Message;
                resp.Data = ex;
            }

            return resp;
        }
    }
}
