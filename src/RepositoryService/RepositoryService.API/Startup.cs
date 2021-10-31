using EventBusRabbitMQ;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RepositoryService.API.Middlewares.RequestResponse;
using RepositoryService.API.RabbitMQ;
using RepositoryService.Application.Handlers;
using RepositoryService.Application.Validators;
using RepositoryService.Core.Models;
using RepositoryService.Infrastructure;
using RepositoryService.Infrastructure.Data;
using RepositoryService.Infrastructure.Data.Interfaces;
using RepositoryService.Infrastructure.Services;
using RepositoryService.Infrastructure.Services.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RepositoryService.API
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
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(Startup));
            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton<AppSettings>(appSettings);

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            services.AddMvc().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<AddRecordCommandValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<RemoveRecordCommandValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<AddContactInfoCommandValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<RemoveContactInfoCommandValidator>();
            });

            // Adding services we created via Infrastructure project's ServiceRegistration.cs
            services.AddInfrastructure();

            #region RabbitMQ Dependency
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }

                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<EventBusRabbitMQConsumer>();
            #endregion

            services.AddHttpContextAccessor();
            services.AddDbContext<PhonebookContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Postgres")));
            services.AddMediatR(typeof(AddRecordHandler).GetTypeInfo().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RepositoryService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PhonebookContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RepositoryService.API v1"));
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
