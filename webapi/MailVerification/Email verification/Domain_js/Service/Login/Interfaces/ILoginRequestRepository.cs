using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_js.Models;

namespace Domain_js.Service.Login.Interfaces
{
    public interface ILoginRequestRepository
    {

        AuthUser GetUserByEmail(string email);
		AuthUser GetUserByEmailpassword(string email,string password);
	}
}
