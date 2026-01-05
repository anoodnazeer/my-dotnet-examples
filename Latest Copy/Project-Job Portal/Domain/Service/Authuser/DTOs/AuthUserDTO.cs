using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Service.Authuser.DTOs
{
    public class AuthUserDTO
    {
        public Guid Id { get; set; }

        public string LegalName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Summary { get; set; }

        public string? Website { get; set; }

        // Image fields
        public byte[]? ProfilePictureData { get; set; }   // For displaying image
        public IFormFile? ProfilePictureFile { get; set; } // For uploading new image

        // Location reference
        public Guid Location { get; set; }

        // Auth-related property
        public string? Password { get; set; }
    }
}
