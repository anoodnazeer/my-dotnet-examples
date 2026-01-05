using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("JobSeekerProfileSkill")]   // 👈 match actual table name in DB
    public class JobSeekerProfileSkill
    {
        public Guid JobSeekerProfileId { get; set; }
        public virtual JobSeekerProfile JobSeekerProfile { get; set; }

        public Guid SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
