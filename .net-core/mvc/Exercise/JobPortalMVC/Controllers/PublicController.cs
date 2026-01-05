using JobPortalMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using JobPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalMVC.Controllers
{
    public class PublicController : Controller  
    {

        private readonly IPublicService publicService;

        public PublicController(IPublicService publicService)
        {
            this.publicService = publicService;
        }

        [HttpGet]
        public IActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult UserRegistration(User user)
        {
            try
            {
                publicService.RegisterUser(user);

                return RedirectToAction("Login");

            }
            catch
            {
                return View();
            }
        }
       

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var user = publicService.loginUser(email, password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());

                    return RedirectToAction("AllJobs", "Job");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}

