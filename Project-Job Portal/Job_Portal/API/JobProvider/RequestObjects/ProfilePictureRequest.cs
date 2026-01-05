using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal.API.JobProvider.RequestObjects
{
    public class ProfilePictureRequest
    {
        [Required]
        [FromForm(Name = "file")] // 👈 ensures Swagger treats it as a file field
        public IFormFile File { get; set; } = null!;
    }
}