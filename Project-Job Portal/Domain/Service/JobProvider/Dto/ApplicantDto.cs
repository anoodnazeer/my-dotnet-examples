using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobProvider.Dto
{
    public class ApplicantDto
    {
        public Guid ApplicationId { get; set; }
        public Guid JobId { get; set; }
        public Guid ApplicantId { get; set; }

        public string ApplicantName { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateApplied { get; set; }
        public string CoverLetter { get; set; }
    }
}
