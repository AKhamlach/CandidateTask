using CandidateTask.Interfaces;
using CandidateTask.Models;

namespace CandidateTask.Tests.Mocks
{
    public class MockDatabase : ICandidateRepository
    {
        private readonly Dictionary<string, Candidate> _candidates = new Dictionary<string, Candidate>();


        public async Task<Candidate> UpsertCandidate(Candidate candidate)
        {
            if (_candidates.ContainsKey(candidate.Email))
            {
                _candidates[candidate.Email].FirstName = candidate.FirstName;
                _candidates[candidate.Email].LastName = candidate.LastName;
                _candidates[candidate.Email].PhoneNumber = candidate.PhoneNumber != null ? candidate.PhoneNumber : _candidates[candidate.Email].PhoneNumber ;
                _candidates[candidate.Email].GitHubProfile = candidate.GitHubProfile != null ? candidate.GitHubProfile : _candidates[candidate.Email].GitHubProfile;
                _candidates[candidate.Email].LinkedInProfile = candidate.LinkedInProfile != null ? candidate.LinkedInProfile : _candidates[candidate.Email].LinkedInProfile;
                _candidates[candidate.Email].PreferedCallInterval = candidate.PreferedCallInterval != null ? candidate.PreferedCallInterval : _candidates[candidate.Email].PreferedCallInterval;
                _candidates[candidate.Email].Comment = candidate.Comment;
            }
            else
            {
                _candidates.Add(candidate.Email, candidate);
            }
            return await Task.FromResult<Candidate>(_candidates[candidate.Email]);
        }



    }
}