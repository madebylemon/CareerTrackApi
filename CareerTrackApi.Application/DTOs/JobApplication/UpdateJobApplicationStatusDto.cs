using System.ComponentModel.DataAnnotations;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Application.DTOs.JobApplication
{
    // DTO yêu cầu cập nhật trạng thái đơn ứng tuyển (PATCH)
    public class UpdateJobApplicationStatusDto
    {
        [Required(ErrorMessage = "Trạng thái đơn ứng tuyển là bắt buộc")]
        [EnumDataType(typeof(ApplicationStatus), ErrorMessage = "Trạng thái không hợp lệ")]
        public ApplicationStatus Status { get; set; }
    }
}
