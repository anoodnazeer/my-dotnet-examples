using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobSeeker.DTOs
{
    public class ApplyJobRequestDto
    {
        public Guid JobPostId { get; set; } // ✅ correct name (Id, not _id)
    
}
}
