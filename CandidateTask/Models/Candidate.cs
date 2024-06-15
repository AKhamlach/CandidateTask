using System.ComponentModel.DataAnnotations;

namespace CandidateTask.Models
{
    public class Candidate
    {

        public int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        
        [RegularExpression(@"^[0-9]*$")]
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string? PreferedCallInterval { get; set; }

        public string? LinkedInProfile { get; set; }

        public string? GitHubProfile { get; set; }
        [Required]
        public required string Comment { get; set; }
    }
}
