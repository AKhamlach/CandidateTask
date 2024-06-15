using CandidateTask.Interfaces;
using CandidateTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        
            private readonly ICandidateRepository _repository;
            private readonly ICacheService _cache;  


        public CandidateController(ICandidateRepository repository, ICacheService cacheService)
            {
                _repository = repository;
            _cache = cacheService;

            }

            [HttpPost]
            public async Task<IActionResult> CreateOrUpdateCandidate([FromBody] Candidate candidate)
            {
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (_cache.TryGetValue<Candidate>(candidate.Email, out var cachedCandidate))
                {
                    return Ok(cachedCandidate);
                }

                Candidate result = await _repository.UpsertCandidate(candidate);

                _cache.Set(candidate.Email, result, TimeSpan.FromMinutes(10));
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500,ex.Message);
            }
            }

    }
}


