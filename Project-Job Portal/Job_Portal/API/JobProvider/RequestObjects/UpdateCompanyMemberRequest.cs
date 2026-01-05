namespace Job_Portal.API.JobProvider.RequestObjects
{
    public class UpdateCompanyMemberRequest
    {
        public string MemberName { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}

