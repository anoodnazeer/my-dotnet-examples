namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class CreateJobSeekerProfileRequest
    {
      //  public Guid JobSeekerId { get; set; }
        public string? ProfileName { get; set; }

        public string? ProfileSummary { get; set; }

        // File uploads from Swagger (form-data)
        public IFormFile? SeekerImage { get; set; }
        public IFormFile? Resume { get; set; }
    }
}
