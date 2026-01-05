using AutoMapper;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.Login.DTO;
using Domain.Service.Login.Interfaces;
using Domain.Service.SignUp;
using Domain.Service.SignUp.DTO;
using Domain.Service.SignUp.Interface;
using Job_Portal.API.JobProvider.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal.API.JobProvider
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class JobProviderController : ControllerBase
    {
        private readonly IJobProviderService _service;
        private readonly IMapper _mapper;
        private readonly ISignUpRequestService _signUpRequestService;
        private readonly ILoginRequestService _loginService;

        public JobProviderController(IJobProviderService service, IMapper mapper, ISignUpRequestService signUpRequestService, ILoginRequestService loginService)
        {
            _service = service;
            _signUpRequestService = signUpRequestService;
            _loginService = loginService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        // 1) Submit signup request
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignUpRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _signUpRequestService.CreateSignupRequest(dto);
            return Ok(new { message = "Signup request submitted successfully. Await verification." });
        }


        [AllowAnonymous]
        // 2) Verify / approve signup request
        [HttpGet("{signUpRequestId}/verify-email")]
        public async Task<IActionResult> Verify(Guid id)
        {
            bool result = await _signUpRequestService.VerifyEmailAsync(id);
            if (!result)
                return NotFound(new { message = "Signup request not found or already verified." });

            return Ok(new { message = "Signup request verified successfully." });
        }
        [AllowAnonymous]
        // 3) Set Password
        [HttpPost("job-provider/sign-up/{signUpRequestId}/set-password")]
        public async Task<IActionResult> SetPassword(Guid signUpRequestId, [FromBody] SetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Password is required." });

            try
            {
                await _signUpRequestService.SetPasswordForLoginAsync(signUpRequestId, request);
                return Ok(new { message = "Password set successfully. You can now login." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        // 3) Login method
        [AllowAnonymous]

        [HttpPost("job-provider/Login")]


        public ActionResult<JobProviderLoginDto> Login([FromBody] JobProviderLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _loginService.Login(request.Email, request.Password);

            if (result == null)
                return Unauthorized(new { Message = "Invalid email or password" });

            return Ok(result);
        }


        // ================== Profile Picture ==================

      
        [HttpPost("{jobProviderId}/profile-picture")]
        public async Task<IActionResult> AddProfilePicture(Guid jobProviderId, [FromForm] ProfilePictureRequest request)
        {
            var message = await _service.AddProfilePictureAsync(jobProviderId, request.File);
            return Ok(new { Message = message });
        }

      
        [HttpPut("update-profile-picture/{jobProviderId}")]

        public async Task<IActionResult> UpdateProfilePicture(Guid jobProviderId, [FromForm] ProfilePictureRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("Please upload a valid image file.");

            var result = await _service.UpdateProfilePictureAsync(jobProviderId, request.File);
            return Ok(new { message = result });
        }



       
        [HttpDelete("{jobProviderId}/profile-picture")]
        public async Task<IActionResult> DeleteProfilePicture(Guid jobProviderId)
        {
            var message = await _service.DeleteProfilePictureAsync(jobProviderId);
            return Ok(new { Message = message });
        }



        
        [HttpGet("{jobProviderId}/profile-picture")]
        public async Task<IActionResult> GetProfilePicture(Guid jobProviderId)
        {
            return await _service.GetProfilePictureAsync(jobProviderId);
        }

        // ================== Company ==================


        
        [HttpPost("{jobProviderId}/company")]
        public async Task<IActionResult> AddCompany(Guid jobProviderId, [FromBody] AddCompanyRequest request)
        {
            var (companyId, message) = await _service.AddCompanyAsync(jobProviderId, request.CompanyName, request.Location, request.Industry, request.WebsiteUrl);
            return Ok(new { CompanyId = companyId, Message = message });
        }


        
        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetCompanyById(Guid companyId)
        {
            var company = await _service.GetCompanyByIdAsync(companyId);
            return Ok(company);
        }
        // ✅ GET ALL COMPANIES


        
        [HttpGet("companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _service.GetAllCompaniesAsync();
            return Ok(companies);
        }


       
        [HttpPut("company/{companyId}")]
        public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] UpdateCompanyRequest request)
        {
            var message = await _service.UpdateCompanyByIdAsync(companyId, request.CompanyName, request.Location, request.Industry, request.WebsiteUrl);
            return Ok(new { Message = message });
        }


       
        [HttpPatch("company/{companyId}")]
        public async Task<IActionResult> PatchCompany(Guid companyId, [FromBody] PatchCompanyRequest request)
        {
            var message = await _service.PatchCompanyByIdAsync(companyId, request.Industry);
            return Ok(new { Message = message });
        }



      
        [HttpDelete("company/{companyId}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var message = await _service.DeleteCompanyByIdAsync(companyId);
            return Ok(new { Message = message });
        }

        // ================== Company Member ==================


       
        [HttpPost("company/{companyId}/member")]
        public async Task<IActionResult> AddCompanyMember(Guid companyId, [FromBody] AddCompanyMemberRequest request)
        {
            var (memberId, message) = await _service.AddCompanyMemberAsync(companyId, request.MemberName, request.Designation, request.Email, request.Phone);
            return Ok(new { MemberId = memberId, Message = message });
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


        
        [HttpPut("company/member/{memberId}")]
        public async Task<IActionResult> UpdateCompanyMember(Guid memberId, [FromBody] UpdateCompanyMemberRequest request)
        {
            var message = await _service.UpdateCompanyMemberAsync(memberId, request.MemberName, request.Designation, request.Email, request.Phone);
            return Ok(new { Message = message });
        }


        
        [HttpPatch("company/member/{memberId}")]
        public async Task<IActionResult> PatchCompanyMember(Guid memberId, [FromBody] PatchCompanyMemberRequest request)
        {
            var message = await _service.PatchCompanyMemberAsync(memberId, request.Designation);
            return Ok(new { Message = message });
        }


        
        [HttpDelete("company/member/{memberId}")]
        public async Task<IActionResult> DeleteCompanyMember(Guid memberId)
        {
            var message = await _service.DeleteCompanyMemberAsync(memberId);
            return Ok(new { Message = message });
        }



       
        [HttpPost("logout/{jobProviderId}")]
        public async Task<IActionResult> Logout(Guid jobProviderId)
        {
            var result = await _service.LogoutAsync(jobProviderId);
            return Ok(new { Message = result });
        }


        // -------------------------
        // JOB APPLICATION ENDPOINTS
        // -------------------------


       
        [HttpGet("jobs/{jobId}/applicants")]
        public async Task<IActionResult> GetApplicantsByJobId(Guid jobId)
        {
            var applicants = await _service.GetApplicantsByJobIdAsync(jobId);
            if (applicants == null || !applicants.Any())
                return NotFound(new { message = "No applicants found for this job" });

            return Ok(applicants);
        }


        
        [HttpGet("applications/{applicationId}")]
        public async Task<IActionResult> GetApplicantByApplicationId(Guid applicationId)
        {
            var applicant = await _service.GetApplicantByApplicationIdAsync(applicationId);
            if (applicant == null)
                return NotFound(new { message = "Applicant not found" });

            return Ok(applicant);
        }


        
        [HttpGet("applications/count")]
        public async Task<IActionResult> GetApplicationCount()
        {
            var count = await _service.GetApplicationCountAsync();
            return Ok(new { totalApplications = count });
        }

    }
}
