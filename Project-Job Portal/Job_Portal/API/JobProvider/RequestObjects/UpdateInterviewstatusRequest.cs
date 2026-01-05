namespace Job_Portal.API.JobProvider.RequestObjects
{
    public class UpdateInterviewStatusRequest
    {
        public string Status { get; set; } = null!; // Scheduled/Completed/Cancelled
    }
}
