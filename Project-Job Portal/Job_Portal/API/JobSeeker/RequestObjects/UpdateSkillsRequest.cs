namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class UpdateSkillsRequest
    {
        public List<Guid> SkillIds { get; set; } = new List<Guid>();

    }
}
