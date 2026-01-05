using System.ComponentModel.DataAnnotations;

namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class PatchWorkExperienceRequest
    {
        [Required]
        public Guid Id { get; set; }   // Required to identify which experience to patch

        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public string? Summary { get; set; }
        public DateTime? ServiceStart { get; set; }
        public DateTime? ServiceEnd { get; set; }
    }
}
