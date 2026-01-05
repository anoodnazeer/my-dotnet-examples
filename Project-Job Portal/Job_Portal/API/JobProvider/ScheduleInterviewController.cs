using AutoMapper;
using Domain.Service.JobProvider.Dto;
using Domain.Service.JobProvider.Interfaces;
using Job_Portal.API.JobProvider.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal.API.JobProvider
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class ScheduleInterviewController : ControllerBase
    {
        private readonly IInterviewService _interviewService;
        private readonly IMapper _mapper;
        public ScheduleInterviewController(IInterviewService interviewService, IMapper mapper)

        {
            _interviewService = interviewService;
            _mapper = mapper;
        }



        // 28. Schedule Interview

       
        [HttpPost]
        public async Task<IActionResult> ScheduleInterview([FromBody] ScheduleInterviewRequest request)
        {
            var interviewDto = _mapper.Map<InterviewDto>(request);
            var interviewId = await _interviewService.ScheduleInterviewAsync(interviewDto);
            return Ok(new { interviewId, message = "Interview scheduled successfully" });
        }


      
        [HttpGet]
        public async Task<IActionResult> GetAllScheduledInterviews()
        {
            var interviews = await _interviewService.GetAllScheduledInterviewsAsync();
            return Ok(interviews);
        }


        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInterviewById(Guid id)
        {
            var interview = await _interviewService.GetInterviewByIdAsync(id);
            return interview == null ? NotFound() : Ok(interview);
        }

        // 30. Update Interview

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterview(Guid id, [FromBody] UpdateInterviewRequest request)
        {
            var interviewDto = _mapper.Map<InterviewDto>(request);
            var result = await _interviewService.UpdateInterviewAsync(id, interviewDto);
            if (!result) return NotFound();

            return Ok(new { message = "Interview updated successfully" });
        }

        // 31. Patch Interview (time only)

     
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchInterview(Guid id, [FromBody] PatchInterviewRequest request)
        {
            var result = await _interviewService.PatchInterviewAsync(id, request.Time);
            if (!result) return NotFound();

            return Ok(new { message = "Interview time updated successfully" });
        }

        // 32. Update Interview Status

       
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateInterviewStatus(Guid id, [FromBody] UpdateInterviewStatusRequest request)
        {
            if (!Guid.TryParse(id.ToString(), out _))  // optional validation
                return BadRequest("Invalid interview ID");

            var result = await _interviewService.UpdateInterviewStatusAsync(id, request.Status);

            if (!result)
                return NotFound(new { message = "Interview not found" });

            return Ok(new { message = "Interview status updated successfully" });
        }

        // 33. Delete Interview

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(Guid id)
        {
            var result = await _interviewService.DeleteInterviewAsync(id);
            if (!result) return NotFound();

            return Ok(new { message = "Interview deleted successfully" });
        }

    }
}
