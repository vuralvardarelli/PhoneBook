﻿using MediatR;
using RepositoryService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Queries
{
    public class GetRecordQuery : IRequest<GenericResult>
    {
        public int RecordId { get; set; }
    }
}
