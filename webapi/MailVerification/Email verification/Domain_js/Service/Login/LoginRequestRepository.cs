using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_js.Models;
using Domain_js.Service.Login.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain_js.Service.Login
{
    public class LoginRequestRepository : ILoginRequestRepository
    {
        protected readonly HireMeNowDbContext _context;
        public LoginRequestRepository(HireMeNowDbContext dbContext)
        {
            _context = dbContext;
        }

        public AuthUser GetUserByEmail(string email)
        {
            var user= _context.AuthUsers.FirstOrDefault(e => e.Email == email);
            return user;
        }
	

		public AuthUser GetUserByEmailpassword(string email, string password)
		{
			var user = _context.AuthUsers.FirstOrDefault(e => e.Email == email && e.Password == password);
			return user;
		}
}
    }
