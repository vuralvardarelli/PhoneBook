using Microsoft.Extensions.DependencyInjection;
using RepositoryService.Infrastructure.Data;
using RepositoryService.Infrastructure.Data.Interfaces;
using RepositoryService.Infrastructure.Services;
using RepositoryService.Infrastructure.Services.Interfaces;

namespace RepositoryService.Infrastructure
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
