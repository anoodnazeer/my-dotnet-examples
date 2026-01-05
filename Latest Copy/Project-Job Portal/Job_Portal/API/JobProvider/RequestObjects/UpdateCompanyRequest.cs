namespace Job_Portal.API.JobProvider.RequestObjects
{
    public class UpdateCompanyRequest
    {
        public string CompanyName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Industry { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
    }
}