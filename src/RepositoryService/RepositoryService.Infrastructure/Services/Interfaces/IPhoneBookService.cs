using RepositoryService.Core.Entities;
using RepositoryService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Infrastructure.Services.Interfaces
{
    public interface IPhoneBookService
    {
        Task AddRecord(Record record);
        Task RemoveRecord(int recordId);
        Task AddContactInfo(int recordId, ContactInfo contactInfo);
        Task RemoveContactInfo(int contactInfoId);
        Task<List<Record>> GetRecords();
        Task<Record> GetRecord(int recordId);
    }
}
