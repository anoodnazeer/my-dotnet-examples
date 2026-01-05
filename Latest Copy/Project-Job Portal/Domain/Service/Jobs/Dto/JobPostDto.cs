using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Jobs.Dto
{
    public class JobPostDto
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobSummary { get; set; } = null!;
        public DateTime PostedDate { get; set; }
        public decimal? Salary { get; set; }
        public string? Experience { get; set; }
        public DateTime? ApplicationDeadline { get; set; }

        public string? JobType { get; set; }

        public Guid LocationId { get; set; }
        public Guid IndustryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid PostedBy { get; set; }
        // Optional: include navigation names for API convenience
        public string? LocationName { get; set; }
        public string? IndustryName { get; set; }
        //public string? CategoryName { get; set; }
        public string? CompanyName { get; set; }
    }
}
