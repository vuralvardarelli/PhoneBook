using Newtonsoft.Json;
using PhoneBook.Core.Models;
using PhoneBook.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient _repositoryClient;
        private HttpClient _reportClient;

        public HttpClientService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _repositoryClient = _clientFactory.CreateClient("repositoryService");
            _reportClient = _clientFactory.CreateClient("reportService");
        }

        //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"advert/get?id={id}");

        //HttpResponseMessage response = await _client.SendAsync(request);

        #region RepositoryService
        public Task AddContactInfo(AddContactInfoCommand request)
        {
            throw new NotImplementedException();
        }

        public Task CreateRecord(AddRecordCommand request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactInfo(RemoveContactInfoCommand request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecord(RemoveRecordCommand request)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> GetRecord(int recordId)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> GetRecords()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ReportService
        public async Task<GenericResult> GetReport(int reportId)
        {
            GenericResult result = new GenericResult();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"report/?reportId={reportId}");

            HttpResponseMessage response = await _reportClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                result.Data = JsonConvert.DeserializeObject<Report>(await response.Content.ReadAsStringAsync());
                result.IsSucceeded = true;
            }
            else
            {
                result.StatusCode = (int)response.StatusCode;
            }

            return result;
        }

        public Task<GenericResult> GetReportDetails(int reportId)
        {
            throw new NotImplementedException();
        }

        public Task RequestReport()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> GetAllReports()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
