using CandidateTask.Models;
using CandidateTask.Tests.Mocks;


namespace CandidateTask.Tests
{
    public class SqlCandidateRepositoryTests
    {
        
        private readonly MockDatabase _repository;

        public SqlCandidateRepositoryTests()
        {
            _repository = new MockDatabase();
        }
        [Fact]
        public async Task AddNewCandidate()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                Email = "john.doe@example.com",
                PreferedCallInterval = "9AM - 5PM",
                LinkedInProfile = "https://linkedin.com/in/johndoe",
                GitHubProfile = "https://github.com/johndoe",
                Comment = "Great candidate!"
            };

            // Act
            var result = await _repository.UpsertCandidate(candidate);

            // Assert
            Assert.Equal(candidate.Email, result.Email);
            Assert.Equal(candidate.FirstName, result.FirstName);
            Assert.Equal(candidate.LastName, result.LastName);
            Assert.Equal(candidate.PhoneNumber, result.PhoneNumber);
            Assert.Equal(candidate.PreferedCallInterval, result.PreferedCallInterval);
            Assert.Equal(candidate.LinkedInProfile, result.LinkedInProfile);
            Assert.Equal(candidate.GitHubProfile, result.GitHubProfile);
            Assert.Equal(candidate.Comment, result.Comment);
        }

        [Fact]
        public async Task UpdateExistingCandidate()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                PhoneNumber = "987-654-3210",
                Email = "jane.smith@example.com",
                PreferedCallInterval = "1PM - 6PM",
                LinkedInProfile = "https://linkedin.com/in/janesmith",
                GitHubProfile = "https://github.com/janesmith",
                Comment = "Another great candidate!"
            };

            await _repository.UpsertCandidate(existingCandidate);

            var updatedCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                PhoneNumber = "987-654-3210",
                Email = "jane.smith@example.com",
                PreferedCallInterval = "2PM - 7PM",
                LinkedInProfile = "https://linkedin.com/in/janesmith-updated",
                GitHubProfile = "https://github.com/janesmith-updated",
                Comment = "Updated comment!"
            };

            // Act
            var result = await _repository.UpsertCandidate(updatedCandidate);

            // Assert
            Assert.Equal(updatedCandidate.Email, result.Email);
            Assert.Equal(updatedCandidate.FirstName, result.FirstName);
            Assert.Equal(updatedCandidate.LastName, result.LastName);
            Assert.Equal(updatedCandidate.PhoneNumber, result.PhoneNumber);
            Assert.Equal(updatedCandidate.PreferedCallInterval, result.PreferedCallInterval);
            Assert.Equal(updatedCandidate.LinkedInProfile, result.LinkedInProfile);
            Assert.Equal(updatedCandidate.GitHubProfile, result.GitHubProfile);
            Assert.Equal(updatedCandidate.Comment, result.Comment);
        }


        [Fact]
        public async Task UpdateExistingCandidateWithIncompleteData()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                PhoneNumber = "987-654-3210",
                Email = "jane.smith@example.com",
                PreferedCallInterval = "1PM - 6PM",
                LinkedInProfile = "https://linkedin.com/in/janesmith",
                GitHubProfile = "https://github.com/janesmith",
                Comment = "Another great candidate!"
            };

            await _repository.UpsertCandidate(existingCandidate);

            var updatedCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PreferedCallInterval = "2PM - 7PM",
                Comment = "Updated comment!"
            };

            // Act
            var result = await _repository.UpsertCandidate(updatedCandidate);

            // Assert
            Assert.Equal(updatedCandidate.Email, result.Email);
            Assert.Equal(updatedCandidate.FirstName, result.FirstName);
            Assert.Equal(updatedCandidate.LastName, result.LastName);
            Assert.Equal(updatedCandidate.PhoneNumber != null ? updatedCandidate.PhoneNumber : existingCandidate.PhoneNumber, result.PhoneNumber);
            Assert.Equal(updatedCandidate.PreferedCallInterval != null ? updatedCandidate.PreferedCallInterval : existingCandidate.PreferedCallInterval, result.PreferedCallInterval);
            Assert.Equal(updatedCandidate.LinkedInProfile != null ? updatedCandidate.LinkedInProfile : existingCandidate.LinkedInProfile, result.LinkedInProfile);
            Assert.Equal(updatedCandidate.GitHubProfile != null ? updatedCandidate.GitHubProfile : existingCandidate.GitHubProfile, result.GitHubProfile);
            Assert.Equal(updatedCandidate.Comment, result.Comment);
        }


    }
}