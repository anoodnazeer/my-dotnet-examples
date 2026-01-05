using JobPortalMVC.Exceptions;
using JobPortalMVC.Interface;
using JobPortalMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortalMVC.Service
{
    public class PublicService : IPublicService 
    {
        public IUserRepository _userRepository;

        public User loggedUser = new User();

        bool _islogged = false;

        public PublicService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User RegisterUser(User user)
        {
            return _userRepository.RegisterUser(user);
        }

        public User loginUser(string email, string password)
        {
            try
            {
                loggedUser = _userRepository.login(email, password);

                if (loggedUser != null)
                {
                    Console.WriteLine("Login Successful");

                    _islogged = true;
                    Console.WriteLine(loggedUser.FirstName);

                    return loggedUser;
                }
                else
                {
                    Console.WriteLine("Log in Filed");

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ServiceException("Technical Error Occured");
            }

        }
    }
}
