using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.JobApplication;
using CareerTrackApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerTrackApi.Api.Controllers
{
    // Controller quản lý đơn ứng tuyển
    [Authorize]
    [Route("api/applications")]
    public class ApplicationsController : BaseApiController
    {
        private readonly IJobApplicationService _service;

        public ApplicationsController(IJobApplicationService service)
        {
            _service = service;
        }

        // Lấy tất cả đơn ứng tuyển của người dùng hiện tại
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = CurrentUserId;
            var apps = await _service.GetAllAsync(userId);
            return Ok(apps);
        }

        // Lấy chi tiết một đơn ứng tuyển
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = CurrentUserId;
            var app = await _service.GetByIdAsync(id, userId);
            if (app == null)
            {
                return NotFound(new { Message = "Không tìm thấy đơn ứng tuyển" });
            }
            return Ok(app);
        }

        // Tạo mới đơn ứng tuyển
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobApplicationRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = CurrentUserId;
            var result = await _service.CreateAsync(request, userId);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // Cập nhật toàn bộ đơn ứng tuyển
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJobApplicationRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = CurrentUserId;
            var result = await _service.UpdateAsync(id, request, userId);
            if (result == null)
            {
                return NotFound(new { Message = "Không tìm thấy đơn ứng tuyển để cập nhật" });
            }

            return Ok(result);
        }

        // Cập nhật trạng thái đơn ứng tuyển (PATCH)
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateJobApplicationStatusDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = CurrentUserId;
            var result = await _service.UpdateStatusAsync(id, request, userId);
            if (result == null)
            {
                return NotFound(new { Message = "Không tìm thấy đơn ứng tuyển để cập nhật trạng thái" });
            }

            return Ok(result);
        }

        // Xóa đơn ứng tuyển
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = CurrentUserId;
            var deleted = await _service.DeleteAsync(id, userId);
            if (!deleted)
            {
                return NotFound(new { Message = "Không tìm thấy đơn ứng tuyển để xóa" });
            }

            return NoContent();
        }
    }
}
