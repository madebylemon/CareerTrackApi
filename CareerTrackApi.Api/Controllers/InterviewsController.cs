using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.Interview;
using CareerTrackApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerTrackApi.Api.Controllers
{
    // Controller quản lý các cuộc phỏng vấn
    [Authorize]
    public class InterviewsController : BaseApiController
    {
        private readonly IInterviewService _service;

        public InterviewsController(IInterviewService service)
        {
            _service = service;
        }

        // Lấy tất cả vòng phỏng vấn của một đơn ứng tuyển
        [HttpGet("api/applications/{applicationId}/interviews")]
        public async Task<IActionResult> GetInterviewsForApplication(int applicationId)
        {
            var userId = CurrentUserId;
            var interviews = await _service.GetAllByApplicationIdAsync(applicationId, userId);
            if (interviews == null)
            {
                return NotFound(new { Message = "Không tìm thấy đơn ứng tuyển tương ứng" });
            }

            return Ok(interviews);
        }

        // Tạo vòng phỏng vấn mới thuộc đơn ứng tuyển
        [HttpPost("api/applications/{applicationId}/interviews")]
        public async Task<IActionResult> CreateInterview(int applicationId, [FromBody] CreateInterviewRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = CurrentUserId;
            var result = await _service.CreateAsync(applicationId, request, userId);
            if (result == null)
            {
                return NotFound(new { Message = "Không tìm thấy đơn ứng tuyển tương ứng để thêm phỏng vấn" });
            }

            return Ok(result);
        }

        // Cập nhật cuộc phỏng vấn theo Id
        [HttpPut("api/interviews/{id}")]
        public async Task<IActionResult> UpdateInterview(int id, [FromBody] UpdateInterviewRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = CurrentUserId;
            var result = await _service.UpdateAsync(id, request, userId);
            if (result == null)
            {
                return NotFound(new { Message = "Không tìm thấy cuộc phỏng vấn để cập nhật" });
            }

            return Ok(result);
        }

        // Xóa cuộc phỏng vấn theo Id
        [HttpDelete("api/interviews/{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
            var userId = CurrentUserId;
            var deleted = await _service.DeleteAsync(id, userId);
            if (!deleted)
            {
                return NotFound(new { Message = "Không tìm thấy cuộc phỏng vấn để xóa" });
            }

            return NoContent();
        }
    }
}
