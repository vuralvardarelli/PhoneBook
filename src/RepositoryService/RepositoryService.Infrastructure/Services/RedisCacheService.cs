using Newtonsoft.Json;
using RepositoryService.Infrastructure.Data.Interfaces;
using RepositoryService.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace RepositoryService.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private ICacheContext _context;

        public RedisCacheService(ICacheContext context)
        {
            _context = context;
        }

        public async Task Add(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            await _context.Redis.StringSetAsync(key, jsonData);
        }

        public async Task<bool> Any(string key)
        {
            return await _context.Redis.KeyExistsAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            if (await Any(key))
            {
                string jsonData = await _context.Redis.StringGetAsync(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        public async Task Remove(string key)
        {
            await _context.Redis.KeyDeleteAsync(key);
        }
    }
}
