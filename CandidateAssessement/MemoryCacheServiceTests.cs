using CandidateTask.Models;
using System;
using System.Collections.Generic;
using CandidateTask.Services;

namespace CandidateTask.Tests
{
    public class MemoryCacheServiceTests
    {
        [Fact]
        public void GetCachedValue()
        {
            // Arrange
            var cache = new CacheService();
            var email = "john.doe@example.com";
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                PhoneNumber = "123-456-7890",
                PreferedCallInterval = "9AM - 5PM",
                LinkedInProfile = "https://linkedin.com/in/johndoe",
                GitHubProfile = "https://github.com/johndoe",
                Comment = "Great candidate!"
            };

            // Act
            cache.Set(email, candidate, TimeSpan.FromMinutes(10));
            var cachedCandidate = cache.Get<Candidate>(email);

            // Assert
            Assert.NotNull(cachedCandidate);
            Assert.Equal(candidate.FirstName, cachedCandidate.FirstName);
            Assert.Equal(candidate.LastName, cachedCandidate.LastName);
            Assert.Equal(candidate.Email, cachedCandidate.Email);
            // Add more assertions as needed
        }

        [Fact]
        public void SetValueToCache()
        {
            // Arrange
            var cache = new CacheService();
            var email = "jane.smith@example.com";
            var candidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = email,
                PhoneNumber = "987-654-3210",
                PreferedCallInterval = "2PM - 6PM",
                LinkedInProfile = "https://linkedin.com/in/janesmith",
                GitHubProfile = "https://github.com/janesmith",
                Comment = "Excellent candidate!"
            };

            // Act
            cache.Set(email, candidate, TimeSpan.FromMinutes(10));
            var cachedCandidate = cache.Get<Candidate>(email);

            // Assert
            Assert.NotNull(cachedCandidate);
            Assert.Equal(candidate.FirstName, cachedCandidate.FirstName);
            Assert.Equal(candidate.LastName, cachedCandidate.LastName);
            Assert.Equal(candidate.Email, cachedCandidate.Email);
            // Add more assertions as needed
        }

        [Fact]
        public void GetNoValueReturnDefault()
        {
            // Arrange
            var cache = new CacheService();
            var email = "nonexistent@example.com";

            // Act
            var cachedCandidate = cache.Get<Candidate>(email);

            // Assert
            Assert.Null(cachedCandidate);
        }

        [Fact]
        public void GetExpiredCache()
        {
            // Arrange
            var cache = new CacheService();
            var email = "expired@example.com";
            var candidate = new Candidate
            {
                FirstName = "Expired",
                LastName = "Candidate",
                Email = email,
                Comment ="Comment",
            };

            cache.Set(email, candidate, TimeSpan.FromSeconds(1)); // Cache for 1 second
            Thread.Sleep(1100); // Wait for cache to expire

            // Act
            var cachedCandidate = cache.Get<Candidate>(email);

            // Assert
            Assert.Null(cachedCandidate);
        }
    }
}