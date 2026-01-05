namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class DeleteSkillsRequest
    {
        public List<Guid> SkillIds { get; set; } = new List<Guid>();
    }
}
