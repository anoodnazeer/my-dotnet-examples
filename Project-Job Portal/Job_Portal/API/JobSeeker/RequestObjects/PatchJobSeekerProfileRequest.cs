namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class PatchJobSeekerProfileRequest
    {
        public string? ProfileName { get; set; }
        public string? ProfileSummary { get; set; }

        public IFormFile? SeekerImage { get; set; }
        public IFormFile? Resume { get; set; }
    }
}
