using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobProvider.Dto
{
    public class JobApplicationDto
    {
        public Guid Id { get; set; }
        public Guid JobPostId { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid ResumeId { get; set; }
        public string CoverLetter { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
