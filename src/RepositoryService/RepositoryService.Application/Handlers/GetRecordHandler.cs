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
    public class GetRecordHandler : IRequestHandler<GetRecordQuery, GenericResult>
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<GetRecordHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetRecordHandler(IPhoneBookService phoneBookService, ILogger<GetRecordHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _phoneBookService = phoneBookService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResult> Handle(GetRecordQuery request, CancellationToken cancellationToken)
        {
            GenericResult resp = new GenericResult();

            try
            {
                resp.Data = await _phoneBookService.GetRecord(request.RecordId);
                resp.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "GetRecordHandler", "Handle", _httpContextAccessor.HttpContext.TraceIdentifier);
                resp.StatusCode = 500;
                resp.Message = ex.Message;
                resp.Data = ex;
            }

            return resp;
        }
    }
}
