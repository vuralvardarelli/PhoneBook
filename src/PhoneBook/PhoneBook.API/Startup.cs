using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhoneBook.API.Middlewares.RequestResponse;
using PhoneBook.Core.Models;
using PhoneBook.Infrastructure.Services;
using PhoneBook.Infrastructure.Services.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace PhoneBook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        new Uri(configuration["AppSettings:ElasticsearchUrl"]))
                    {
                        CustomFormatter = new ElasticsearchJsonFormatter(),
                        IndexFormat = configuration["AppSettings:ElasticsearchIndex"]
                    })
                .MinimumLevel.Verbose()
                .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton<AppSettings>(appSettings);

            services.AddHttpContextAccessor();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            #region Repository Microservice HttpClient Init
            services.AddHttpClient("repositoryService", c =>
            {
                c.BaseAddress = new Uri(appSettings.RepositoryServiceUrl);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            #endregion

            #region Report Microservice HttpClient Init
            services.AddHttpClient("reportService", c =>
            {
                c.BaseAddress = new Uri(appSettings.ReportServiceUrl);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            #endregion

            services.AddScoped<IHttpClientService, HttpClientService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneBook.API v1"));
            }

            app.UseRequestResponseLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
