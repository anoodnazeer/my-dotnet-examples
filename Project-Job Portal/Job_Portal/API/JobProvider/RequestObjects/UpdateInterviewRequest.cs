namespace Job_Portal.API.JobProvider.RequestObjects
{
    public class UpdateInterviewRequest
    {
        public DateTime Date { get; set; }
        public string Time { get; set; } = null!;
        public string Mode { get; set; } = null!;
     
    }
}