using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Service.Profile.DTOs
{
    public class JobseekerProfileSkillDto
    {
        public Guid JobSeekerProfileId { get; set; }

        public Guid JobSeekerId { get; set; }
        public List<Guid> SkillIds { get; set; } = new List<Guid>();
    }
}
