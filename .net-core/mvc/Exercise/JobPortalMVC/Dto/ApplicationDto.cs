using JobPortalMVC.Models;

namespace JobPortalMVC.Dto
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
         

        public User? User { get; set; }
        public Job? Job { get; set; }
    }
}
