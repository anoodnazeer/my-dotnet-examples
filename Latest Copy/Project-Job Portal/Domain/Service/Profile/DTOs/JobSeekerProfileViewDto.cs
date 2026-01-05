using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Profile.DTOs
{
    public partial  class JobSeekerProfileViewDto
    {
        public Guid Id { get; set; }
        public Guid JobSeekerId { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileSummary { get; set; }

        // For display purpose
        public string? ImageBase64 { get; set; }
        public string? ResumeBase64 { get; set; }
    }
}
