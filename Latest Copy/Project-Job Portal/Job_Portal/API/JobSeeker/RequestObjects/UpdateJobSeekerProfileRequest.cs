namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class UpdateJobSeekerProfileRequest
    {
        public string? ProfileName { get; set; }
        public string? ProfileSummary { get; set; }

        // Optional file updates
        public IFormFile? SeekerImage { get; set; }
        public IFormFile? Resume { get; set; }
    }
}
