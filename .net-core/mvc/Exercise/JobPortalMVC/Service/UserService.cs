using JobPortalMVC.Interface;
using JobPortalMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortalMVC.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User GetBiId(int guid)
        {
            return repository.getById(guid);
        }
    }
}
