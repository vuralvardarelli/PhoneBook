using MediatR;
using RepositoryService.Application.Responses;
using RepositoryService.Core.Entities;
using System.Collections.Generic;

namespace RepositoryService.Application.Commands
{
    public class AddRecordCommand : IRequest<RecordResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
