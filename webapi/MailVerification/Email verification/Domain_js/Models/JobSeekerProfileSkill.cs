using System;

namespace Domain_js.Models
{
    public class JobSeekerProfileSkill
    {
        public Guid JobSeekerProfileId { get; set; }
        public virtual JobSeekerProfile JobSeekerProfile { get; set; }

        public Guid SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
