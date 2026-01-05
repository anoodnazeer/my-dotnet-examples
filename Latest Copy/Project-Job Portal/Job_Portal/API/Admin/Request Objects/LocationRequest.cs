namespace Job_Portal.API.Admin.Request_Objects
{
    public class LocationRequest
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string City { get; set; }           // make nullable
        public string State { get; set; }          // make nullable
        public string Country { get; set; }
    }
}
