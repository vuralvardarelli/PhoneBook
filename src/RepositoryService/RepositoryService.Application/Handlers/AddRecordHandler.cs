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

namespace RepositoryService.Application.Handlers
{
    public class AddRecordHandler : IRequestHandler<AddRecordCommand, RecordResponse>
    {
        private readonly PhonebookContext _context;
        private static object _lock = new object();

        public AddRecordHandler(PhonebookContext context)
        {
            _context = context;
        }

        public async Task<RecordResponse> Handle(AddRecordCommand request, CancellationToken cancellationToken)
        {
            var recordEntity = RecordMapper.Mapper.Map<Record>(request);
            if (recordEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            Monitor.Enter(_lock);
            _context.Add(recordEntity);
            _context.SaveChanges();
            Record record = _context.Records.OrderByDescending(x => x.RecordId).FirstOrDefault();
            Monitor.Exit(_lock);

            var recordResponse = RecordMapper.Mapper.Map<RecordResponse>(record);
            return recordResponse;
        }
    }
}
