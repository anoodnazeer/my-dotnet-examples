using System.ComponentModel.DataAnnotations;

namespace ImageAndResume.RequestObject
{
    public class FileCreateRequest
    {
        [Required]
        public IFormFile File { get; set; }


        // Optional additional fields used only at insert time
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
