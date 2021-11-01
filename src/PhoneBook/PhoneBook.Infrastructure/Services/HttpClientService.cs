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

        public HttpClientService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory, HttpClient repositoryClient, HttpClient reportClient, ILogger<HttpClientService> logger)
        {
            _clientFactory = clientFactory;
            _repositoryClient = _clientFactory.CreateClient("repositoryService");
            _reportClient = _clientFactory.CreateClient("reportService");
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }



        #region RepositoryService
        public async Task<GenericResult> AddContactInfo(AddContactInfoCommand request)
        {
            GenericResult result = new GenericResult();

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _repositoryClient.PostAsync("repository/AddContactInfo", stringContent);

            try
            {
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "AddContactInfo", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> CreateRecord(AddRecordCommand request)
        {
            GenericResult result = new GenericResult();

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _repositoryClient.PostAsync("repository/AddRecord", stringContent);

            try
            {
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "CreateRecord", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> DeleteContactInfo(RemoveContactInfoCommand request)
        {
            GenericResult result = new GenericResult();

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _repositoryClient.PostAsync("repository/RemoveContactInfo", stringContent);

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
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "DeleteContactInfo", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> DeleteRecord(RemoveRecordCommand request)
        {
            GenericResult result = new GenericResult();

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _repositoryClient.PostAsync("repository/RemoveRecord", stringContent);

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
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "DeleteRecord", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> GetRecord(int recordId)
        {
            GenericResult result = new GenericResult();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"repository/GetRecord?recordId={recordId}");

            HttpResponseMessage response = await _repositoryClient.SendAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result.Data = JsonConvert.DeserializeObject<Record>(await response.Content.ReadAsStringAsync());
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
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "GetRecord", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
        }

        public async Task<GenericResult> GetRecords()
        {
            GenericResult result = new GenericResult();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"repository/GetRecords");

            HttpResponseMessage response = await _repositoryClient.SendAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result.Data = JsonConvert.DeserializeObject<List<Record>>(await response.Content.ReadAsStringAsync());
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
                _logger.LogError(Constants.ErrorLoggingTemplate, ex.GetType().Name, ex.Message, ex.StackTrace, "HttpClientService", "GetRecords", _httpContextAccessor.HttpContext.TraceIdentifier);
                result.StatusCode = (int)response.StatusCode;
                result.Message = ex.Message;
                result.Data = ex;
            }

            return result;
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
