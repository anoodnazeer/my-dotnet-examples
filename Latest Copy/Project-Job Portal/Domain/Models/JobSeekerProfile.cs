using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class JobSeekerProfile
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }   

        [ForeignKey(nameof(JobSeeker))]
        public Guid JobSeekerId { get; set; }

        public string? ProfileName { get; set; }
        public byte[]? SeekerImage { get; set; }  
 
        public byte[]? Resume { get; set; }

        public string? ProfileSummary { get; set; }

        [JsonIgnore]
        public virtual JobSeeker? JobSeeker { get; set; }


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
