namespace Job_Portal.API.Admin.Request_Objects
{
    public class JobProviderRequestDto
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; }
        public string Email { get; set; }
        public string Summary { get; set; }
        public string Website { get; set; }
        public string LocationName { get; set; }
    }
}
