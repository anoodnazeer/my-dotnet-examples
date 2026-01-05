using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Admin.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string City { get; set; }           // make nullable
        public string State { get; set; }          // make nullable
        public string Country { get; set; }
    }
}
