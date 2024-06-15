using CandidateTask.Interfaces;

namespace CandidateTask.Services
{
    public class CacheService : ICacheService
    {
        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, CacheEntry<object>> _cache;

        public CacheService()
        {
            _cache = new System.Collections.Concurrent.ConcurrentDictionary<string, CacheEntry<object>>();
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out CacheEntry<object> entry) && !entry.IsExpired())
            {
                return (T)entry.Value;
            }
            return default;
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            _cache.AddOrUpdate(key, new CacheEntry<object>(value, expiration),
                (k, existingEntry) => new CacheEntry<object>(value, expiration));
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default;
            if (_cache.TryGetValue(key, out CacheEntry<object> entry) && !entry.IsExpired())
            {
                value = (T)entry.Value;
                return true;
            }
            return false;
        }

        public void Remove(string key)
        {
            _cache.TryRemove(key, out _);
        }

        private class CacheEntry<T>
        {
            public T Value { get; }
            private DateTime Expiration { get; }

            public CacheEntry(T value, TimeSpan expiresIn)
            {
                Value = value;
                Expiration = DateTime.UtcNow.Add(expiresIn);
            }

            public bool IsExpired() => DateTime.UtcNow >= Expiration;
        }
    }
}
