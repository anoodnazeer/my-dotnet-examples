using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.JobSeeker.DTOs
{
    public class SavedJobDto
    {
        public Guid SavedJobId { get; set; }     
        public Guid JobId { get; set; }          
        public string JobTitle { get; set; }    
        public DateTime DateSaved { get; set; } 
    }


}
