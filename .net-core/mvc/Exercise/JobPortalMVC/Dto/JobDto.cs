using System.ComponentModel.Design;

namespace JobPortalMVC.Dto
{
    public class JobDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Company { get; set; } = "";
        public string Location { get; set; } = "";
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
        public string Responsibilities { get; set; } = "";
        public string SalaryRange { get; set; } = "";


        public JobDto(string title, string description, string location, string salary, string company )
        {
             
            Title = title;
            Description = description;
            Location = location;
            SalaryRange = salary;
            Company = company; 
        }

        public JobDto()
        {
        }
    }
}
