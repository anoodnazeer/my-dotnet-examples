using System.ComponentModel.DataAnnotations;

namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class UpdateWorkExperienceRequest
    {
        [Required]
        public Guid Id { get; set; }   // WorkExperience ID

        [Required]
        public string JobTitle { get; set; } = null!;

        [Required]
        public string CompanyName { get; set; } = null!;

        [Required]
        public string Summary { get; set; } = null!;

        [Required]
        public DateTime ServiceStart { get; set; }

        [Required]
        public DateTime ServiceEnd { get; set; }
    }
}
