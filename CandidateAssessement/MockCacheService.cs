using CandidateTask.Interfaces;


namespace CandidateTask.Tests.Mocks
{
    public class MockCacheService : ICacheService
    {
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return (T)value;
            }
            return default;
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            _cache[key] = value;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default;
            if (_cache.TryGetValue(key, out var cachedValue))
            {
                value = (T)cachedValue;
                return true;
            }
            return false;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
