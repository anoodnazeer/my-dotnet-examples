using System.ComponentModel.DataAnnotations;

namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class ApplyJobRequest
    {
        [Required]
        public Guid JobPostId { get; set; } // ✅ matches entity exactly

    }
}
