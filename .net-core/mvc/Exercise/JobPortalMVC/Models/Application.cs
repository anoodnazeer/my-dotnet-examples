namespace JobPortalMVC.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
        public Job? Job { get; set; }
    }
}
