using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Service.JobProvider.Dto;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IInterviewRepository
    {
        Task<Guid> ScheduleInterviewAsync(Interview interview);
        Task<List<InterviewDto>> GetAllScheduledInterviewsAsync();
        Task<Interview?> GetInterviewByIdAsync(Guid id);
        Task<bool> UpdateInterviewAsync(Guid id, Interview updatedInterview);
        Task<bool> PatchInterviewAsync(Guid id, string time);
        Task<bool> UpdateInterviewStatusAsync(Guid id, string status);
        Task<bool> DeleteInterviewAsync(Guid id);
    }
}
