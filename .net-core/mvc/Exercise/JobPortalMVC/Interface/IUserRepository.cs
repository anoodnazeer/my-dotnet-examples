using JobPortalMVC.Models;

namespace JobPortalMVC.Interface
{
    public interface  IUserRepository
    {
        User RegisterUser(User user);

        User login(string email, string password);
        User getLoggedUser();

        User getById(int uid);

    }
}
