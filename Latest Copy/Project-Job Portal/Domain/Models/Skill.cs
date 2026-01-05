using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Skill
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Navigation property
        public virtual ICollection<JobSeekerProfileSkill> JobSeekerProfileSkills { get; set; }
            = new List<JobSeekerProfileSkill>();
    }
}
