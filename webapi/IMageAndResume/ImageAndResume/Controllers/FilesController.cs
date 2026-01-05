using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageAndResume.Dto;
using ImageAndResume.Service;
using ImageAndResume.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageAndResume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _service;


        public FilesController(IFileService service)
        {
            _service = service;
        }

        [HttpPost]
        [RequestSizeLimit(30_000_000)] // 30 MB request limit
        public async Task<IActionResult> Create([FromForm] FileCreateRequest request)
        {
            try
            {
                var dto = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(Download), new { id = dto.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        [RequestSizeLimit(30_000_000)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] FileUpdateRequest request)
        {
            try
            {var updated = await _service.UpdateAsync(id, request);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FileDto>> GetById(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("{id:guid}/download")]
        public async Task<IActionResult> Download(Guid id)
        {
            var content = await _service.GetFileContentAsync(id);
            if (content == null) return NotFound();


            return File(content.Value.Data, content.Value.ContentType, content.Value.FileName);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
