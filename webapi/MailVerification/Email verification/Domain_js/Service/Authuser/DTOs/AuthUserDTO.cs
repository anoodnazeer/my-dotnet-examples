using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service.Authuser.DTOs
{
    public class AuthUserDTO
    {
        public Guid JobseekerId { get; set; }
        public string? UserName { get; set; }

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; }
        public IFormFile? Image { get; set; } // Added for image upload
        public string? Phone { get; set; }

        public string? Password { get; set; }
    }
}
