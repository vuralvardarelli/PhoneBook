using MediatR;
using RepositoryService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Commands
{
    public class RemoveRecordCommand : IRequest<GenericResult>
    {
        public int RecordId { get; set; }
    }
}
