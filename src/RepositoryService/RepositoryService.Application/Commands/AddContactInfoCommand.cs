using MediatR;
using RepositoryService.Core.Entities;
using RepositoryService.Core.Models;

namespace RepositoryService.Application.Commands
{
    public class AddContactInfoCommand : IRequest<GenericResult>
    {
        public int RecordId { get; set; }
        public ContactInfo ContactInfo { get; set; }

    }
}
