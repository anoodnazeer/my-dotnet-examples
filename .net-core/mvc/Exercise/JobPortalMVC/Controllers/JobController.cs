using AutoMapper;
using JobPortalMVC.Dto;
using JobPortalMVC.Interface;
using JobPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortalMVC.Controllers
{
    public class JobController : Controller
    {
        IMapper _mapper;
        IJobRepository _jobRepository;
        IJobService _jobService;
        IUserRepository _userRepository;

        public JobController(IMapper mapper, IJobRepository jobRepository, IJobService jobService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobService = jobService;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AllJobs()
        {
            var uid = HttpContext.Session.GetString("UserId");

            User user = _userRepository.getById(Convert.ToInt32(uid));

            List<Job> Jobs = _jobService.GetJobs();

            return View(Jobs);
        }

        [HttpGet]
        public IActionResult ViewJob(int jobid)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            List<Job> Jobs = _jobService.GetJobSelected(jobid);

            return View(Jobs);
        }


        public async Task<IActionResult> ViewJob(int? id)
        {
            if (id == null) return NotFound();

            var job = await _jobService.GetJobSelected.FirstOrDefaultAsync(j => j.Id == id.Value);
            if (job == null) return NotFound();

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyJob(int jobId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Public");
            }
            await _jobService.ApplyJobAsync(userId.Value, jobId);
            TempData["Success"] = "Your application has been submitted.";
            return RedirectToAction("ViewJob", new { jobId });
        }
    }
}
