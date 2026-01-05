namespace Job_Portal.API.Jobs.RequestObjects
{
    public class CreateJobPostRequest
    {
        public Guid CompanyId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobSummary { get; set; } = null!;
        public Guid LocationId { get; set; }
        public Guid IndustryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PostedBy { get; set; }
        public string? Experience { get; set; } // optional
        public decimal? Salary { get; set; }    // optional
        public string? JobType { get; set; }    // optional
        public DateTime? ApplicationDeadline { get; set; } // optional
    }
}
