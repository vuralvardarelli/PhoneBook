using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using PhoneBook.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PhoneBook.API.Middlewares.RequestResponse
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            // Start of request logging
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation(Constants.RequestLoggingTemplate, "Request", context.Request.Scheme, context.Request.Host, context.Request.Path, context.Request.Method, DateTime.UtcNow, context.Request.QueryString, ReadStreamInChunks(requestStream));
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            // Start of requests processing
            await _next(context);

            // Start of response logging
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation(Constants.ResponseLoggingTemplate, "Response", context.Request.Scheme, context.Request.Host, context.Request.Path, DateTime.UtcNow, context.Response.StatusCode, responseBodyText);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        /// <summary>
        /// For reading Request in chunks
        /// </summary>
        /// <param name="stream">input request stream</param>
        /// <returns></returns>
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
