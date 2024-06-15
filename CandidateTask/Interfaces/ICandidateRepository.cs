using CandidateTask.Models;

namespace CandidateTask.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> UpsertCandidate(Candidate candidate);
        
    }
}
