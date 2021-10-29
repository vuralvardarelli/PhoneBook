using MediatR;
using RepositoryService.Core.Models;

namespace RepositoryService.Application.Queries
{
    public class GetRecordsQuery : IRequest<GenericResult>
    {
    }
}
