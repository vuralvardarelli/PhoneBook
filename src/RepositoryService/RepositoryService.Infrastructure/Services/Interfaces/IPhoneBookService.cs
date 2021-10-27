using RepositoryService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Infrastructure.Services.Interfaces
{
    public interface IPhoneBookService
    {
        Record AddRecord(Record record);
    }
}
