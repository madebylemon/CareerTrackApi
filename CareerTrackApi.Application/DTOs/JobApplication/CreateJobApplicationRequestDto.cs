using System;
using System.ComponentModel.DataAnnotations;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Application.DTOs.JobApplication
{
    // DTO yêu cầu tạo đơn ứng tuyển mới
    public class CreateJobApplicationRequestDto
    {
        [Required(ErrorMessage = "Tên công ty là bắt buộc")]
        [StringLength(150, ErrorMessage = "Tên công ty không vượt quá 150 ký tự")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vị trí ứng tuyển là bắt buộc")]
        [StringLength(150, ErrorMessage = "Vị trí ứng tuyển không vượt quá 150 ký tự")]
        public string RoleTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái đơn ứng tuyển là bắt buộc")]
        [EnumDataType(typeof(ApplicationStatus), ErrorMessage = "Trạng thái không hợp lệ")]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Saved;

        [Required(ErrorMessage = "Ngày nộp đơn là bắt buộc")]
        public DateTime DateApplied { get; set; }

        [StringLength(1000, ErrorMessage = "Ghi chú không vượt quá 1000 ký tự")]
        public string? Notes { get; set; }
    }
}
