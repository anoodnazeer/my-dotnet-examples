using Domain_js.Models;

public partial class JobPost
{
    public Guid Id { get; set; }
    public string JobTitle { get; set; }
    public string JobSummary { get; set; }
    public DateTime PostedDate { get; set; }

    public Guid LocationId { get; set; }
    public Guid IndustryId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid PostedBy { get; set; }

    public virtual Location Location { get; set; }
    public virtual Industry Industry { get; set; }
    public virtual JobCategory Category { get; set; }
    public virtual JobProviderCompany Company { get; set; }
    public virtual CompanyUser PostedByNavigation { get; set; }

    public virtual ICollection<JobResponsibility> JobResponsibilities { get; set; } = new List<JobResponsibility>();
}
