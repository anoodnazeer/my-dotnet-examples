using JobPortalMVC.Models;

namespace JobPortalMVC.Interface
{
    public interface  IUserService
    {
        User GetBiId(int guid);
    }
}
