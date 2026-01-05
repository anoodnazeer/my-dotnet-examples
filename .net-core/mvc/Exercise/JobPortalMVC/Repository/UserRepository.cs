using System.Data;
using JobPortalMVC.Interface;
using JobPortalMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortalMVC.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        private static User loggedUser = new User();
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }



        public User RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User login(string email, string password)
        {
            loggedUser = _context.Users.Where(e => e.Email == email && e.Password == password).FirstOrDefault();
            return loggedUser;
        }

        public User getLoggedUser()
        {
            return loggedUser;
        }

        public User getById(int userid)
        {
            User user = _context.Users.Where(e => e.Id == userid)
                .IgnoreAutoIncludes().FirstOrDefault();

            return user;
        }


    }
}
