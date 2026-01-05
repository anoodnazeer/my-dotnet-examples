using AutoMapper;
using Domain.Service.Jobs.Dto;
using Domain.Service.Jobs.Interfaces;
using Job_Portal.API.Jobs.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal.API.Jobs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class PostJobController : ControllerBase
    {
        private readonly IJobService _service;
        private readonly IMapper _mapper;

        public PostJobController(IJobService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        // -------------------------
        // JOB POST ENDPOINTS
        // -------------------------

      
        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJobPost([FromBody] CreateJobPostRequest request)
        {
            var jobPostDto = _mapper.Map<JobPostDto>(request);
            var jobId = await _service.CreateJobPostAsync(jobPostDto);
            return Ok(new { jobId, message = "Job created successfully" });
        }


      
        [HttpGet("jobs/{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var job = await _service.GetJobByIdAsync(id);
            if (job == null)
                return NotFound();

            return Ok(job);
        }


        [HttpPut("jobs/{id}")]
        public async Task<IActionResult> UpdateJobById(Guid id, [FromBody] UpdateJobPostRequest request)
        {
            var updatedJobDto = _mapper.Map<JobPostDto>(request);
            var result = await _service.UpdateJobByIdAsync(id, updatedJobDto);
            if (!result) return NotFound("Job not found");

            return Ok(new { message = "Job updated successfully" });
        }



        [HttpPatch("jobs/{id}")]
        public async Task<IActionResult> PatchJobById(Guid id, [FromBody] PatchJobPostRequest request)
        {
            var result = await _service.PatchJobByIdAsync(id, request.Salary);
            if (!result) return NotFound(new { message = "Job not found" });
            return Ok(new { message = "Job updated successfully" });
        }


        [HttpDelete("jobs/{id}")]
        public async Task<IActionResult> DeleteJobById(Guid id)
        {
            var result = await _service.DeleteJobByIdAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Job deleted successfully" });
        }


      
        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _service.GetAllJobsAsync();
            return Ok(jobs);
        }


    }

}
