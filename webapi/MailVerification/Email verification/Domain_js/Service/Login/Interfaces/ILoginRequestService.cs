using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_js.Service.Login.DTOs;

namespace Domain_js.Service.Login.Interfaces
{
    public interface ILoginRequestService
    {

        JobSeekerLoginDto login(string email, string password);

       
    }
}
