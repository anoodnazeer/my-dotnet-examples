using System.Security.Claims;
using AutoMapper;
using Domain.Models;

using Domain.Service.Authuser.Interfaces;
using Domain.Service.Profile;
using Domain.Service.Profile.DTOs;
using Domain.Service.Profile.Interface;

using Job_Portal.API.JobSeeker.RequestObjects;
using Job_Portal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal.API.JobSeeker
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_SEEKER")]
    public class JobSeekerProfileController : BaseApiController<JobSeekerProfileController>

    {
        private readonly IJobSeekerProfileService _profileService;
        public IMapper mapper { get; set; }
        public IAuthUserService authUserService { get; set; }
        public JobSeekerProfileController(IJobSeekerProfileService profileService, IMapper _mapper, IAuthUserService _authUserService)
        {
            mapper = _mapper;
            _profileService = profileService;
            authUserService = _authUserService;
        }


        [Authorize]
        [HttpPost("Create-Profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProfile([FromForm] CreateJobSeekerProfileRequest request)
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);

            var seekerprofiledto = mapper.Map<JobSeekerProfileDto>(request);
            var result = await _profileService.CreateProfileAsync(seekerprofiledto, jobSeekerId);
            return Ok(result);
        }




        [Authorize]
        [HttpPut("Update-Profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateJobSeekerProfileRequest request)
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var seekerprofiledto = mapper.Map<JobSeekerProfileDto>(request);

            var result = await _profileService.UpdateProfileAsync(seekerprofiledto, jobSeekerId);
            return Ok(result);
        }




        [Authorize]
        [HttpPatch("Patch-Profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PatchProfile([FromForm] PatchJobSeekerProfileRequest request)
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var seekerprofiledto = mapper.Map<JobSeekerProfileDto>(request);

            var result = await _profileService.PatchProfileAsync(seekerprofiledto, jobSeekerId);

            if (result == null)
                return NotFound("Profile not found for this Job Seeker.");

            return Ok(result);
        }




        [Authorize]
        [HttpGet("View-Profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);

            var result = await _profileService.GetProfileByJobSeekerIdAsync(jobSeekerId);

            if (result == null)
                return NotFound("Profile not found for this Job Seeker.");

            return Ok(result);
        }




        [Authorize]
        [HttpDelete("Delete-Profile")]
        public async Task<IActionResult> DeleteMyProfile()
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);

            var result = await _profileService.DeleteProfileAsync(jobSeekerId);


            if (result.Contains("Cannot delete"))
                return BadRequest(result);
            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }




        [Authorize]
        [HttpGet("View-Resume")]
        public async Task<IActionResult> ViewMyResume()
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            if (jobSeekerId == null)
                return Unauthorized("Invalid or missing JobSeeker ID in token.");

            var resumeData = await _profileService.GetResumeByJobSeekerIdAsync(jobSeekerId);
            if (resumeData == null)
                return NotFound("Resume not found for this JobSeeker.");


            string contentType = "application/pdf";
            return File(resumeData, contentType, "Resume.pdf");
        }




        [Authorize]
        [HttpGet("Get all-skills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _profileService.GetAllSkillsAsync();
            return Ok(skills.Select(s => new { s.Id, s.Name, s.Description }));
        }




        [Authorize]
        [HttpPost("Add-skills")]
        public async Task<IActionResult> AddSkills([FromBody] AddJobSeekerSkillsRequest request)
        {

            var Skilldto = mapper.Map<JobseekerProfileSkillDto>(request);

            if (Skilldto.SkillIds == null || request.SkillIds.Count == 0)
                return BadRequest("Please select at least one skill.");

            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var result = await _profileService.AddSkillsToJobSeekerAsync(jobSeekerId, Skilldto.SkillIds);
            return Ok(result);
        }




        //[Authorize]
        //[HttpPut("Update skills")]
        //public async Task<IActionResult> UpdateSkills([FromBody] UpdateSkillsRequest request)
        //{
        //    var Skilldto = mapper.Map<JobseekerProfileSkillDto>(request);

        //    if (Skilldto.SkillIds == null || !request.SkillIds.Any())
        //        return BadRequest("Please select at least one skill.");

        //    var userId = authUserService.GetUserId();
        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("Invalid or missing JobSeeker ID.");

        //    Guid jobSeekerId = Guid.Parse(userId);


        //    bool success = await _profileService.UpdateSkillsAsync(jobSeekerId, Skilldto.SkillIds);

        //    if (!success)
        //        return NotFound("JobSeeker profile not found.");

        //    return Ok("Skills updated successfully.");
        //}




        //[Authorize]
        //[HttpPatch("Patch skills")]
        //public async Task<IActionResult> PatchSkills([FromBody] PatchSkillsRequest request)
        //{
        //    if ((request.AddSkillIds == null || !request.AddSkillIds.Any()) &&
        //        (request.RemoveSkillIds == null || !request.RemoveSkillIds.Any()))
        //    {
        //        return BadRequest("Please provide skills to add or remove.");
        //    }

        //    var userId = authUserService.GetUserId();
        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("Invalid or missing JobSeeker ID.");

        //    Guid jobSeekerId = Guid.Parse(userId);


        //    bool success = await _profileService.PatchSkillsAsync(jobSeekerId, request.AddSkillIds, request.RemoveSkillIds);

        //    if (!success)
        //        return NotFound("JobSeeker profile not found.");

        //    return Ok("Skills updated successfully (patch).");
        //}




        [Authorize]
        [HttpDelete("Delete skills")]
        public async Task<IActionResult> DeleteSkills([FromBody] DeleteSkillsRequest request)
        {
            if (request.SkillIds == null || !request.SkillIds.Any())
                return BadRequest("Please provide at least one skill ID to delete.");

            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);

            bool success = await _profileService.DeleteSkillsAsync(jobSeekerId, request.SkillIds);

            if (!success)
                return NotFound("No matching skills found for deletion.");

            return Ok("Selected skills deleted successfully.");
        }




        [Authorize]
        [HttpGet("Get skills")]
        public async Task<IActionResult> GetJobSeekerSkills()
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var skills = await _profileService.GetSkillsByJobSeekerIdAsync(jobSeekerId);

            if (skills == null || !skills.Any())
                return Ok(new List<SkillDto>());

            return Ok(skills);
        }




        [Authorize]
        [HttpPost("AddWorkExperience")]
        public async Task<IActionResult> AddWorkExperience([FromBody] AddWorkExperienceRequest request)
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var dto = mapper.Map<WorkExperienceDto>(request);
            var result = await _profileService.AddWorkExperienceAsync(jobSeekerId, dto);

            return Ok(result);
        }




        [Authorize]
        [HttpPut("UpdateWorkExperience")]
        public async Task<IActionResult> UpdateWorkExperience([FromBody] UpdateWorkExperienceRequest request)
        {
            
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var dto = mapper.Map<WorkExperienceDto>(request);
            var result = await _profileService.UpdateWorkExperienceAsync(jobSeekerId, dto);

            return Ok(result);
        }




        [Authorize]
        [HttpPatch("PatchWorkExperience")]
        public async Task<IActionResult> PatchWorkExperience([FromBody] PatchWorkExperienceRequest request)
        {
            
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var dto = mapper.Map<WorkExperienceDto>(request);
            var result = await _profileService.PatchWorkExperienceAsync(jobSeekerId, dto);

            return Ok(result);
        }




        [Authorize]
        [HttpGet("ViewWorkexperience")]
        public async Task<IActionResult> ViewWorkExperience()
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);


            var result = await _profileService.GetWorkExperienceByJobSeekerIdAsync(jobSeekerId);

            if (result == null || !result.Any())
                return NotFound("No work experience found for this job seeker.");

            return Ok(result);
        }




        [Authorize]
        [HttpDelete("DeleteWorkexperience")]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {
            try
            {
           
                var userId = authUserService.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid or missing JobSeeker ID.");

                Guid jobSeekerId = Guid.Parse(userId);

                bool deleted = await _profileService.DeleteWorkExperienceAsync(id, jobSeekerId);

                if (!deleted)
                    return BadRequest("Failed to delete work experience.");

                return Ok("Work experience deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [Authorize]
        [HttpPost("AddQualification")]
        public async Task<IActionResult> AddQualification([FromBody] QualificationAddRequest request)
        {
            var userId = authUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing JobSeeker ID.");

            Guid jobSeekerId = Guid.Parse(userId);

            var dto = mapper.Map<QualificationDto>(request);
            var result = await _profileService.AddQualificationAsync(jobSeekerId, dto);
            return Ok(result);
        }




        [Authorize]
        [HttpPut("UpdateQualification/{id}")]
        public async Task<IActionResult> UpdateQualification(Guid id, [FromBody] QualificationUpdateRequest request)
        {
            try
            {
                
                var userId = authUserService.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid or missing JobSeeker ID.");

                Guid jobSeekerId = Guid.Parse(userId);


                var dto = mapper.Map<QualificationDto>(request);
                var updated = await _profileService.UpdateQualificationAsync(id, jobSeekerId, dto);

                if (updated == null)
                    return NotFound("Qualification not found.");

                return Ok(updated);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [Authorize]
        [HttpPatch("PatchQualification")]
        public async Task<IActionResult> PatchQualification(Guid id, QualificationPatchRequest request)
        {
            try
            {
                
                var userId = authUserService.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid or missing JobSeeker ID.");

                Guid jobSeekerId = Guid.Parse(userId);


                var dto = mapper.Map<QualificationDto>(request);
                var updated = await _profileService.PatchQualificationAsync(id, jobSeekerId, dto);

                if (updated == null)
                    return NotFound("Qualification not found.");

                return Ok(updated);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [Authorize]
        [HttpGet("ViewQualification")]
        public async Task<IActionResult> ViewQualifications()
        {
            try
            {

                var userId = authUserService.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid or missing JobSeeker ID.");

                Guid jobSeekerId = Guid.Parse(userId);


                var result = await _profileService.GetQualificationsByJobSeekerIdAsync(jobSeekerId);

                if (result == null || !result.Any())
                    return NotFound("No qualifications found for this job seeker.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [Authorize]
        [HttpDelete("DeleteQualification")]
        public async Task<IActionResult> DeleteQualification(Guid qualificationId)
        {
            try
            {

                var userId = authUserService.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid or missing JobSeeker ID.");

                Guid jobSeekerId = Guid.Parse(userId);


                var result = await _profileService.DeleteQualificationAsync(qualificationId, jobSeekerId);
                if (!result)
                    return BadRequest("Failed to delete qualification.");

                return Ok("Qualification deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
