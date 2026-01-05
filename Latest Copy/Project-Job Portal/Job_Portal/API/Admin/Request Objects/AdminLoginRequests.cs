
using System.ComponentModel.DataAnnotations;

namespace Job_Portal.API.Admin.Request_Objects
{
    public class AdminLoginRequests
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
