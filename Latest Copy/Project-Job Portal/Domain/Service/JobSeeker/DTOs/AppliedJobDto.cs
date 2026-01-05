using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobSeeker.DTOs
{
    public class AppliedJobDto
    {
        public Guid JobApplicationId { get; set; }
        public Guid JobPostId { get; set; }
        public string JobTitle { get; set; }
        public string JobSummary { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
