using PhoneBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Infrastructure.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<GenericResult> CreateRecord(AddRecordCommand request);
        Task<GenericResult> DeleteRecord(RemoveRecordCommand request);
        Task<GenericResult> AddContactInfo(AddContactInfoCommand request);
        Task<GenericResult> DeleteContactInfo(RemoveContactInfoCommand request);
        Task<GenericResult> GetRecord(int recordId);
        Task<GenericResult> GetRecords();


        Task<GenericResult> RequestReport();
        Task<GenericResult> GetAllReports();
        Task<GenericResult> GetReport(int reportId);
        Task<GenericResult> GetReportDetails(int reportId);
    }
}
