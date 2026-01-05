using Domain_js.Models;

public partial class Industry
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
}
