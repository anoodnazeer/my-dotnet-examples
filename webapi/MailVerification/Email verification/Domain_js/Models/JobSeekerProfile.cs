using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain_js.Models
{
    public partial class JobSeekerProfile
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }  // EF handles this; don't set Guid.NewGuid() here

        public Guid? ResumeId { get; set; }

        [ForeignKey(nameof(JobSeeker))]
        public Guid JobSeekerId { get; set; }

        public string? ProfileName { get; set; }
        public string? ProfileSummary { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Resume? Resume { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }

        [JsonIgnore]
        public virtual ICollection<JobSeekerProfileSkill> JobSeekerProfileSkills { get; set; }
            = new List<JobSeekerProfileSkill>();

        [JsonIgnore]
        public virtual ICollection<Qualification> Qualifications { get; set; }
            = new List<Qualification>();

        [JsonIgnore]
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
            = new List<WorkExperience>();
    }
}
