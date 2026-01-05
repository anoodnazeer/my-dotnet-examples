using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login.DTO
{
    public class JobProviderLoginDto
    {
        public Guid Id { get; set; }
        public Guid? JobProviderId { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "JOB_PROVIDER"; // Default role
        public string Email { get; set; } = string.Empty;
        public string? Token { get; set; }

        public JobProviderLoginDto() { }
    }
}