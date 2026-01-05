using JobPortalMVC.Models;

namespace JobPortalMVC.Interface
{
    public interface  IPublicService
    {
        public User RegisterUser(User newJobSeeker);

        public User loginUser(string email, string password);
    }
}
