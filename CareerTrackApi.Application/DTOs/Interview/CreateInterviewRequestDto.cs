using System;
using System.ComponentModel.DataAnnotations;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Application.DTOs.Interview
{
    // DTO yêu cầu tạo vòng phỏng vấn mới
    public class CreateInterviewRequestDto
    {
        [Required(ErrorMessage = "Loại phỏng vấn là bắt buộc")]
        [EnumDataType(typeof(InterviewType), ErrorMessage = "Loại phỏng vấn không hợp lệ")]
        public InterviewType InterviewType { get; set; }

        [Required(ErrorMessage = "Thời gian phỏng vấn là bắt buộc")]
        public DateTime ScheduledAt { get; set; }

        [StringLength(1000, ErrorMessage = "Ghi chú không vượt quá 1000 ký tự")]
        public string? Notes { get; set; }
    }
}
