using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

public partial class JobPost
{
    public Guid Id { get; set; }
    public string JobTitle { get; set; }
    public string JobSummary { get; set; }
    public DateTime PostedDate { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }
    public string Experience { get; set; } = null!;
    public DateTime ApplicationDeadline { get; set; }
    public string JobType { get; set; } = null!;
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
    public string Status { get; set; } = "Pending";//new Line Added by me

    public virtual ICollection<JobResponsibility> JobResponsibilities { get; set; } = new List<JobResponsibility>();
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
