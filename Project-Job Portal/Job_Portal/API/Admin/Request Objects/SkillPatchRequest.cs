namespace Job_Portal.API.Admin.Request_Objects
{
    public class SkillPatchRequest
    {
        public string? Name { get; set; } // ✅ Nullable — not required
        public string? Description { get; set; }
    }
}
