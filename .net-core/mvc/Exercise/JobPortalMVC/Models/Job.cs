using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobPortalMVC.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Company { get; set; } = "";
        public string Type { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";
        public string Responsibilities { get; set; } = "";
        public string SalaryRange { get; set; } = "";
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    }
}
