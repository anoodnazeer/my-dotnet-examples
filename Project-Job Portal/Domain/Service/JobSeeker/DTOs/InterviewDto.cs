using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobSeeker.DTOs
{
    public class InterviewDto
    {
        public Guid Id { get; set; }
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? Date { get; set; }
    }

}
