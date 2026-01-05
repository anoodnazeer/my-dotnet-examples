namespace Job_Portal.API.JobProvider.RequestObjects
{
    public class ScheduleInterviewRequest
    {
        public Guid JobId { get; set; }
        public Guid JobSeekerId { get; set; }
        public DateTime ScheduledDateTime { get; set; }  // Combine date & time
                                                         //public string Time { get; set; } = null!;
        public string Mode { get; set; } = null!; // Online/Offline
                                                  //public string Link { get; set; } = null!;

    }
}
