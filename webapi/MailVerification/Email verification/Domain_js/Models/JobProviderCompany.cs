using Domain_js.Models;

public partial class JobProviderCompany
{
    public Guid Id { get; set; }
    public string LegalName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Summary { get; set; }
    public string Website { get; set; }

    public Guid Location { get; set; }
    public virtual Location LocationNavigation { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
}
