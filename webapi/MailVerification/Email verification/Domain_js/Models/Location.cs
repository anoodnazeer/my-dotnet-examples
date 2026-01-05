using System;
using System.Collections.Generic;

namespace Domain_js.Models;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Discription { get; set; } = null!;

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual ICollection<JobProviderCompany> JobProviderCompanies { get; set; } = new List<JobProviderCompany>();
}
