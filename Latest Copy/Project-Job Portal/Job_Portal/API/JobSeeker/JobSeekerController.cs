
using AutoMapper;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.Jobs;
using Domain.Service.Jobs.Interfaces;
using Domain.Service.JobSeeker;
using Domain.Service.JobSeeker.DTOs;
using Domain.Service.JobSeeker.Interfaces;
using Domain.Service.Login.Interfaces;
using Job_Portal.API.JobSeeker.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
﻿using System.Security.Claims;

namespace Job_Portal.API.JobSeeker
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_SEEKER")]
    public class JobSeekerController : ControllerBase
    {
        public IJobSeekerService jobSeekerService { get; set; }
        private readonly IInterviewService _interviewService;

        public IJobService jobService;
        public ILoginRequestService loginRequestService { get; set; }
        public IAuthUserService  authUserService { get; set; }
        public IMapper mapper { get; set; }
        public JobSeekerController(IInterviewService interviewService,IJobSeekerService _jobSeekerService, IMapper _mapper, ILoginRequestService _loginRequestService, IAuthUserService _authUserService , IJobService _jobService)
        {
            jobSeekerService = _jobSeekerService;
            loginRequestService = _loginRequestService;
            authUserService = _authUserService;
            _interviewService = interviewService;
            mapper = _mapper;
            jobService = _jobService;
             
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult> createJobSeekerSignupRequest(JobSeekerSignupRequest data)
        {
            var jobSeekerSignupRequestDto = mapper.Map<JobSeekerSignupRequestDto>(data);
            jobSeekerService.CreateSignupRequest(jobSeekerSignupRequestDto);
            return Ok(data);
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("Verify-email")]
        public async Task<ActionResult> VerifyJobSeekerEmail(Guid jobSeekerSignupRequestId)
        {
            var isVerified = await jobSeekerService.VerifyEmailAsync(jobSeekerSignupRequestId);
            if (isVerified)
            {
                return Ok();
            }
            return BadRequest();
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("Set-password")]
        public async Task<ActionResult> createJobSeekerSignupRequest(Guid jobSeekerSignupRequestId, [FromBody] string password)
        {
            await jobSeekerService.CreateJobseeker(jobSeekerSignupRequestId, password);
            return Ok("Password Set Successfully");
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] JobSeekerLoginRequest logdata)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request.");

            var user = await loginRequestService.LoginJS(logdata.Email, logdata.Password);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            return Ok(user);
        }


//......................................................................................................................

        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await jobService.GetAllJobsAsync();
            if (jobs == null || !jobs.Any())
                return NotFound("No jobs found.");

            return Ok(jobs);
        }




        [HttpGet("GetJobById/{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var job = await jobService.GetJobByIdAsync(id);
            if (job == null)
                return NotFound("Job not found.");

            return Ok(job);
        }




        [HttpGet("GetJobByTitle/{title}")]
        public async Task<IActionResult> GetJobByTitle(string title)
        {
            var job = await jobSeekerService.GetJobByTitleAsync(title);
            if (job == null)
                return NotFound("Job not found.");

            return Ok(job);
        }


//.....................................................................................................................................


        [HttpPost]
        [Route("Job-application")]
        public async Task<IActionResult> ApplyJob([FromBody] ApplyJobRequest request)
        {
            var userId = authUserService.GetUserId();
            var jobSeekerId = Guid.Parse(userId);

            bool alreadyApplied = await jobSeekerService.HasAlreadyAppliedAsync(jobSeekerId, request.JobPostId);
            if (alreadyApplied)
                return BadRequest(new { message = "You have already applied for this job." });

            var dto = mapper.Map<ApplyJobRequestDto>(request);
            var result = await jobSeekerService.ApplyJobAsync(jobSeekerId, dto);

            if (result)
                return Ok(new { message = "Job Applied Successfully" });

            return BadRequest(new { message = "Failed to apply for job." });
        }




        
        [HttpGet]
        [Route("Get Applied-jobs")]
        public async Task<IActionResult> GetAppliedJobs()
        {
            var userId = authUserService.GetUserId();
            var jobSeekerId = Guid.Parse(userId);

            var appliedJobs = await jobSeekerService.GetAppliedJobsAsync(jobSeekerId);
            return Ok(appliedJobs);
        }




       
        [HttpGet]
        [Route("Search applied-jobs by Title")]
        public async Task<IActionResult> GetAppliedJobsByTitle([FromQuery] string title)
        {
            var userId = authUserService.GetUserId();
            var jobSeekerId = Guid.Parse(userId);

            var appliedJobs = await jobSeekerService.GetAppliedJobsByTitleAsync(jobSeekerId, title);
            return Ok(appliedJobs);
        }




        
        [HttpDelete]
        [Route("Cancel job-application")]
        public async Task<IActionResult> CancelAppliedJob(Guid jobApplicationId)
        {
            var jobSeekerId = Guid.Parse(authUserService.GetUserId());

            var result = await jobSeekerService.CancelAppliedJobAsync(jobSeekerId, jobApplicationId);

            if (result)
                return Ok(new { Message = "Application cancelled successfully." });

            return NotFound(new { Message = "Application not found or does not belong to the user." });
        }




      
        [HttpPost("SaveJob")]
        public async Task<IActionResult> SaveJob(Guid jobId)
        {
            var jobSeekerId = new Guid(authUserService.GetUserId());

            var dto = new SavedJobDto
            {
                JobId = jobId,
                DateSaved = DateTime.UtcNow
            };
            var result = await jobSeekerService.SaveJobAsync(jobSeekerId, dto);
            if (result)
                return Ok("Job saved successfully");

            return BadRequest("Job does not exist or could not be saved.");
        }




        
        [HttpGet("Get saved-jobs")]
        public async Task<IActionResult> GetSavedJobs()
        {
            var jobSeekerId = Guid.Parse(authUserService.GetUserId());
            var result = await jobSeekerService.GetSavedJobsAsync(jobSeekerId);
            return Ok(result);
        }




       
        [HttpGet("Search saved-jobs")]
        public async Task<IActionResult> GetSavedJobsByTitle([FromQuery] string title)
        {
            var jobSeekerId = Guid.Parse(authUserService.GetUserId());
            var result = await jobSeekerService.GetSavedJobsByTitleAsync(jobSeekerId, title);
            return Ok(result);
        }




     
        [HttpDelete("Remove saved-job")]
        public async Task<IActionResult> RemoveSavedJob(Guid savedJobId)
        {
            var jobSeekerId = Guid.Parse(authUserService.GetUserId());
            var result = await jobSeekerService.RemoveSavedJobAsync(jobSeekerId, savedJobId);

            if (result)
                return Ok(new { Message = "Saved job removed successfully" });

            return NotFound(new { Message = "Saved job not found" });
        }




     
        [HttpGet("Get scheduled-interviews")]
      
        public async Task<IActionResult> GetAllScheduledInterviews()
        {
            var interviews = await _interviewService.GetAllScheduledInterviewsAsync();
            return Ok(interviews);
        }





        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userIdString = authUserService.GetUserId();

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized("Invalid user ID.");

            await authUserService.LogoutAsync(userId);
            return Ok("Logout successful.");
        }

    }
}
 

