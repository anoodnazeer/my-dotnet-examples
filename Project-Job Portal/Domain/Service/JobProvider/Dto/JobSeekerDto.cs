using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobProvider.Dto
{
    public class JobSeekerDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Title { get; set; }
        public int Role { get; set; }
        // Optional: omit Image for API or include as Base64
        public byte[]? Image { get; set; }
    }
}
