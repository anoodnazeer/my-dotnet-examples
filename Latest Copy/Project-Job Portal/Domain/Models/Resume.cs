using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Resume
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public byte[]? File { get; set; }
    [JsonIgnore]
    public virtual ICollection<JobSeekerProfile> JobSeekerProfiles { get; set; } = new List<JobSeekerProfile>();
}
