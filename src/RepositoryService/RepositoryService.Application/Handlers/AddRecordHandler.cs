using MediatR;
using RepositoryService.Application.Commands;
using RepositoryService.Application.Mapper;
using RepositoryService.Application.Responses;
using RepositoryService.Core.Entities;
using RepositoryService.Infrastructure.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using RepositoryService.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using RepositoryService.Core;

namespace RepositoryService.Application.Handlers
{
    public class AddRecordHandler : IRequestHandler<AddRecordCommand, RecordResponse>
    {
        private readonly PhonebookContext _context;
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<AddRecordHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddRecordHandler(PhonebookContext context, IPhoneBookService phoneBookService, ILogger<AddRecordHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _phoneBookService = phoneBookService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RecordResponse> Handle(AddRecordCommand request, CancellationToken cancellationToken)
        {
            RecordResponse resp = null;

            try
            {
                Record recordEntity = RecordMapper.Mapper.Map<Record>(request);
                if (recordEntity == null)
                    throw new ApplicationException($"Entity could not be mapped.");

                recordEntity = _phoneBookService.AddRecord(recordEntity);

                resp = RecordMapper.Mapper.Map<RecordResponse>(recordEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "AddRecordHandler", "Handle", _httpContextAccessor.HttpContext.TraceIdentifier);
            }

            return resp;
        }
    }
}
