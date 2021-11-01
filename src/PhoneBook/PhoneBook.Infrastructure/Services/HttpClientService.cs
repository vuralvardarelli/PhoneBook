using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneBook.Core;
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
        private readonly ILogger<HttpClientService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientService(IHttpContextAccessor httpContextAccessor,IHttpClientFactory clientFactory, HttpClient repositoryClient, HttpClient reportClient, ILogger<HttpClientService> logger)
        {
            _clientFactory = clientFactory;
            _repositoryClient = _clientFactory.CreateClient("repositoryService"); 
            _reportClient = _clientFactory.CreateClient("reportService");
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }



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

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result.Data = JsonConvert.DeserializeObject<Report>(await response.Content.ReadAsStringAsync());
                    result.IsSucceeded = true;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;
                    result.Message = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "GetReport", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> GetReportDetails(int reportId)
        {
            GenericResult result = new GenericResult();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"report/GetReportDetails?reportId={reportId}");

            HttpResponseMessage response = await _reportClient.SendAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result.Data = JsonConvert.DeserializeObject<List<ReportDetail>>(await response.Content.ReadAsStringAsync());
                    result.IsSucceeded = true;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;
                    result.Message = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "GetReportDetails", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> RequestReport()
        {
            GenericResult result = new GenericResult();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"report/RequestReport/");

            HttpResponseMessage response = await _reportClient.SendAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result.IsSucceeded = true;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;
                    result.Message = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "RequestReport", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> GetAllReports()
        {

            GenericResult result = new GenericResult();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"report/ListReports");

            HttpResponseMessage response = await _reportClient.SendAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result.Data = JsonConvert.DeserializeObject<List<Report>>(await response.Content.ReadAsStringAsync());
                    result.IsSucceeded = true;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;
                    result.Message = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "GetAllReports", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }
        #endregion
    }
}
