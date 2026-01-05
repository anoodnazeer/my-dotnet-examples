namespace Job_Portal.API.Admin.Request_Objects
{
    public class LocationPatchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string City { get; set; }           // make nullable
        public string State { get; set; }          // make nullable
        public string Country { get; set; }

    }
}
