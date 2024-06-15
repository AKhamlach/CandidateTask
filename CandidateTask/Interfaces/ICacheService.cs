﻿namespace CandidateTask.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiration);
        bool TryGetValue<T>(string key, out T value);
        void Remove(string key);
    }
}
