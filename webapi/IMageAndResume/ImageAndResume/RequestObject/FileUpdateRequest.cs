namespace ImageAndResume.RequestObject
{
    public class FileUpdateRequest
    {
        public IFormFile? File { get; set; }


        // Optional fields to update metadata
        public string? Description { get; set; }
    }
}
