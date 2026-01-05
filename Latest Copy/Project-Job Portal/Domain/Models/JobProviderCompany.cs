using Domain.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

public partial class JobProviderCompany
{
    public Guid Id { get; set; }
    public string LegalName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Summary { get; set; }
    public string Website { get; set; }

    public Guid? Location { get; set; }           // nullable
    public virtual Location? LocationNavigation { get; set; } // nullable navigation

    public byte[]? ProfilePictureData { get; set; }

    [NotMapped]
    public IFormFile? ProfilePictureFile { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
}
