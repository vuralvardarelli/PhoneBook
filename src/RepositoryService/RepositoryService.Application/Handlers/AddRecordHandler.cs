using MediatR;
using RepositoryService.Application.Commands;
using RepositoryService.Application.Mapper;
using RepositoryService.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using RepositoryService.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using RepositoryService.Core;
using RepositoryService.Core.Models;

namespace RepositoryService.Application.Handlers
{
    public class AddRecordHandler : IRequestHandler<AddRecordCommand, GenericResult>
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<AddRecordHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddRecordHandler(IPhoneBookService phoneBookService, ILogger<AddRecordHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _phoneBookService = phoneBookService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResult> Handle(AddRecordCommand request, CancellationToken cancellationToken)
        {
            GenericResult resp = new GenericResult();

            try
            {
                Record recordEntity = RecordMapper.Mapper.Map<Record>(request);
                if (recordEntity == null)
                    throw new ApplicationException($"Entity could not be mapped.");

                await _phoneBookService.AddRecord(recordEntity);

                resp.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "AddRecordHandler", "Handle", _httpContextAccessor.HttpContext.TraceIdentifier);
                resp.StatusCode = 500;
                resp.Message = ex.Message;
                resp.Data = ex;
            }

            return resp;
        }
    }
}
