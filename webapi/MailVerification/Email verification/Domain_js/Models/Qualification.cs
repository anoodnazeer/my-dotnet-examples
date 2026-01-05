using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_js.Models;

    public partial class Qualification
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Guid? JobseekerProfileId { get; set; }

        public Guid? JobPostId { get; set; }

        public virtual JobPost? JobPost { get; set; }
        public JobSeekerProfile JobSeekerProfile { get; set; }  
    }
