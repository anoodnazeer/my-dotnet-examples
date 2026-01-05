using System.ComponentModel.DataAnnotations;

namespace ImageAndResume.Models
{
    public class FileDocument
    {
        [Key]
        public Guid Id { get; set; }


        [Required]
        [MaxLength(260)]
        public string FileName { get; set; }


        [Required]
        [MaxLength(200)]
        public string ContentType { get; set; }


        [Required]
        public long Size { get; set; }


        [Required]
        public byte[] Data { get; set; }


        public string? Description { get; set; }


        public DateTime UploadedOn { get; set; }
    }
}
