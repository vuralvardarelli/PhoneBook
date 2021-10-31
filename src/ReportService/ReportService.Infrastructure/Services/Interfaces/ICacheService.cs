using System.Threading.Tasks;

namespace ReportService.Infrastructure.Services.Interfaces
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);
        Task Add(string key, object data);
        Task AddWithExpire(string key, object data, int second);
        Task Remove(string key);
        Task<bool> Any(string key);
    }
}
