using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Service.Profile.DTOs
{
    public partial class JobSeekerProfileDto
    {
        public Guid Id { get; set; }
        public Guid JobSeekerId { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileSummary { get; set; }
         
        // File uploads from Swagger (form-data)
        public IFormFile? SeekerImage { get; set; }
        public IFormFile? Resume { get; set; }
    }
}
