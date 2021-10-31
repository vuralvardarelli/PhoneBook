using Microsoft.Extensions.DependencyInjection;
using ReportService.Infrastructure.Data;
using ReportService.Infrastructure.Data.Interfaces;
using ReportService.Infrastructure.Services;
using ReportService.Infrastructure.Services.Interfaces;

namespace ReportService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICacheContext, CacheContext>();
            services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}
