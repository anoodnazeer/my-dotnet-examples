using AutoMapper;
using Domain.Service.Admin.Interfaces;
using Domain.Service.Authuser;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.JobSeeker.Interfaces;
using Domain.Service.Login.Interfaces;
using Job_Portal.API.Admin.Request_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal.API.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class AdminFetchController : ControllerBase
    {
        private readonly IAdminServices _adminService;
        
        public IJobSeekerService jobSeekerService { get; set; }

        private readonly IMapper _mapper;
        IAdminRepository _adminRepository;
        private IMapper mapper;
        private readonly ILoginRequestService _loginRequestService;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IJobProviderService _service;
        public AdminFetchController( IMapper _mapper, ILoginRequestService _loginRequestService, IAuthUserService _authUserService,IJobProviderService service,IMapper mapper, IAdminServices adminService, IAdminRepository adminRepostory, ILoginRequestService loginRequestService, IJobSeekerRepository jobSeekerRepository)
        {
            _mapper = mapper;
          
            _adminService = adminService;
            _adminRepository = adminRepostory;
            _loginRequestService = loginRequestService;
            _jobSeekerRepository = jobSeekerRepository;
            _service = service;
            

        }
        // ✅ 1. Get all JobSeekerProfiles
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAllJobSeekers")]
        public async Task<IActionResult> GetAllJobSeekers()
        {
            var jobSeekers = await _jobSeekerRepository.GetAllAsync();
            return Ok(jobSeekers);
        }

        // ✅ 2. Get JobSeekerProfile by Id
        [HttpGet("GetJobSeekerById/{id}")]
        public async Task<IActionResult> GetJobSeekerById(Guid id)
        {
            var jobSeekerProfile = await _jobSeekerRepository.GetByIdAsync(id);
            if (jobSeekerProfile == null)
                return NotFound($"JobSeekerProfile with ID {id} not found.");

            return Ok(jobSeekerProfile);
        }

        // ✅ 3. Delete JobSeekerProfile by Id
        [HttpDelete("DeleteJobSeekerById/{id}")]
        public async Task<IActionResult> DeleteJobSeekerById(Guid id)
        {
            var deleted = await _jobSeekerRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound($"JobSeekerProfile with ID {id} not found or already deleted.");

            return Ok($"JobSeekerProfile with ID {id} deleted successfully.");
        }
        
        // ✅ 4. Get total JobSeeker count
        [HttpGet("GetJobSeekerCount")]
        public async Task<IActionResult> GetJobSeekerCount()
        {
            var count = await _jobSeekerRepository.GetCountAsync();
            return Ok(new { TotalJobSeekers = count });
        }
        // ✅ Get total job count
        [HttpGet("Jobcount")]
        public async Task<IActionResult> GetJobCount()
        {
            var count = await _adminService.GetJobCountAsync();
            return Ok(new { TotalJobs = count });
        }


        // ✅ Get job by name
        [HttpGet("getbyname/{jobTitle}")]
        public async Task<IActionResult> GetJobByName(string jobTitle)
        {
            var job = await _adminService.GetJobByNameAsync(jobTitle);

            if (job == null)
                return NotFound(new { Message = $"No job found with title '{jobTitle}'." });

            return Ok(job);
        }

        [HttpGet("GetAllJobProviders")]
        public async Task<IActionResult> GetAllJobProviders()
        {
            var providers = await _adminService.GetAllProviders();
            var allproviders = _mapper.Map<List<JobProviderRequestDto>>(providers);
            return Ok(providers);
        }


        [HttpGet("GetJobProviderById/{id}")]

        public async Task<IActionResult> GetJobProviderById(Guid id)
        {
            var jobprovider = await _adminService.GetJobProviderByIdAsync(id);

            if (jobprovider == null)
                return NotFound("JobProvider not found");
            var providerById = _mapper.Map<JobProviderRequestDto>(jobprovider);
            return Ok(providerById);
        }


        // ✅ Get total jobProvider count
        [HttpGet("JobProvidercount")]
        public async Task<IActionResult> GetJobProviderCount()
        {
            var count = await _adminService.GetJobProviderCountAsync();
            //return Ok(count);
            return Ok(new { TotalJobProvider = count });
        }


        [HttpDelete("DeleteJobProvider/{id}")]

        public async Task<IActionResult> DeleteJobProvider(Guid id)
        {
            var deleted = await _adminService.DeleteJobProviderAsync(id);

            if (!deleted)
                return NotFound("JobProvider not found.");

            return Ok(new { Message = "JobProvider deleted successfully." });
        }

        [HttpGet("company/member/{memberId}")]
        public async Task<IActionResult> GetCompanyMemberById(Guid memberId)
        {
            var member = await _service.GetCompanyMemberByIdAsync(memberId);

            if (member == null)
                return NotFound(new { Message = "Company member not found" });

            return Ok(member);
        }
        // ✅ GET ALL COMPANY MEMBERS




        [HttpGet("company-members")]
        public async Task<IActionResult> GetAllCompanyMembers()
        {
            var members = await _service.GetAllCompanyMembersAsync();
            return Ok(members);
        }

        [HttpDelete("company/member/{memberId}")]
        public async Task<IActionResult> DeleteCompanyMember(Guid memberId)
        {
            var message = await _service.DeleteCompanyMemberAsync(memberId);
            return Ok(new { Message = message });
        }

      



    }
}


    
