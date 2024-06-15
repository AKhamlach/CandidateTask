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


            public CandidateController(ICandidateRepository repository)
            {
                _repository = repository;

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
                Candidate result = await _repository.UpsertCandidate(candidate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500,ex.Message);
            }
            }

    }
}


