using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RepositoryService.Application.Commands;
using RepositoryService.Core;
using RepositoryService.Core.Models;
using RepositoryService.Infrastructure.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryService.Application.Handlers
{
    public class RemoveRecordHandler : IRequestHandler<RemoveRecordCommand, GenericResult>
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<RemoveRecordHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveRecordHandler(IPhoneBookService phoneBookService, ILogger<RemoveRecordHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _phoneBookService = phoneBookService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResult> Handle(RemoveRecordCommand request, CancellationToken cancellationToken)
        {
            GenericResult resp = new GenericResult();

            try
            {
                await _phoneBookService.RemoveRecord(request.RecordId);

                resp.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "RemoveRecordHandler", "Handle", _httpContextAccessor.HttpContext.TraceIdentifier);
                resp.StatusCode = 500;
                resp.Message = ex.Message;
                resp.Data = ex;
            }

            return resp;
        }
    }
}
