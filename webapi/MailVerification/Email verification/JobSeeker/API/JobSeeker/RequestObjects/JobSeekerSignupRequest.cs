using System.ComponentModel.DataAnnotations;

namespace JobSeeker.API.JobSeeker.RequestObjects
{
    public class JobSeekerSignupRequest
    {

        public string? UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
