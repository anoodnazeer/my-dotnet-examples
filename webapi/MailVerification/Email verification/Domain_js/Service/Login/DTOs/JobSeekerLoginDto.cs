using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service.Login.DTOs
{
    public class JobSeekerLoginDto
    {

        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Role {  get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string? Token { get; set; }
        public JobSeekerLoginDto()
        {
                
        }
    }
}
