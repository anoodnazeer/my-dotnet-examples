namespace ImageAndResume.Dto
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string? Description { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}
