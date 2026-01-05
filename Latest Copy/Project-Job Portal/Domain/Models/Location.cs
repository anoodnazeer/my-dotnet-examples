using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string? City { get; set; }           // make nullable
    public string? State { get; set; }          // make nullable
    public string? Country { get; set; }        // make nullable

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual ICollection<JobProviderCompany> JobProviderCompanies { get; set; } = new List<JobProviderCompany>();
}
