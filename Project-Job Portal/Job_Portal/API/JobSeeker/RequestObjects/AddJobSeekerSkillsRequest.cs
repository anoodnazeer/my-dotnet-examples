namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class AddJobSeekerSkillsRequest
    {
        public List<Guid> SkillIds { get; set; } = new List<Guid>();
    }
}
