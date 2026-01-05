using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobProvider.Dto
{
    public class InterviewDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string? JobTitle { get; set; }
        public Guid JobSeekerId { get; set; }
        public string? JobSeekerName { get; set; }
        public DateTime DateScheduled { get; set; }
        public string Mode { get; set; } = null!;
        public string Status { get; set; } = "Scheduled";
        public Guid CompanyId { get; set; }
    }
}
