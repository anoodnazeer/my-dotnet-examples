namespace Job_Portal.API.JobSeeker.RequestObjects
{
    public class PatchSkillsRequest
    {
        public List<Guid> AddSkillIds { get; set; } = new List<Guid>();
        public List<Guid> RemoveSkillIds { get; set; } = new List<Guid>();
    }
}
