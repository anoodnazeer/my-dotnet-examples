using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.JobProvider.Dto;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IInterviewService
    {
        Task<Guid> ScheduleInterviewAsync(InterviewDto interviewDto);
        Task<List<InterviewDto>> GetAllScheduledInterviewsAsync();
        Task<InterviewDto?> GetInterviewByIdAsync(Guid id);
        Task<bool> UpdateInterviewAsync(Guid id, InterviewDto updatedInterview);
        Task<bool> PatchInterviewAsync(Guid id, string time);
        Task<bool> UpdateInterviewStatusAsync(Guid id, string status);
        Task<bool> DeleteInterviewAsync(Guid id);
    }
}
