using Domain.Models;
using Domain.Service.Jobs.Dto;
using Domain.Service.JobSeeker.DTOs;

namespace Domain.Service.JobSeeker.Interfaces
{
    public interface IJobSeekerService
    {
        void CreateSignupRequest(JobSeekerSignupRequestDto data);
        Task<bool> VerifyEmailAsync(Guid jobSeekerSignupRequestId);
        Task CreateJobseeker(Guid jobSeekerSignupRequestId, string password);
        Task<bool> ApplyJobAsync(Guid jobSeekerId, ApplyJobRequestDto requestDto);
        Task<bool> HasAlreadyAppliedAsync(Guid jobSeekerId, Guid jobPostId);
        Task<List<AppliedJobDto>> GetAppliedJobsAsync(Guid jobSeekerId);
        Task<List<AppliedJobDto>> GetAppliedJobsByTitleAsync(Guid jobSeekerId, string jobTitle);
        Task<bool> CancelAppliedJobAsync(Guid jobSeekerId, Guid jobApplicationId);
        Task<bool> SaveJobAsync(Guid jobSeekerId, SavedJobDto dto);
        Task<List<SavedJobDto>> GetSavedJobsAsync(Guid jobSeekerId);
        Task<List<SavedJobDto>> GetSavedJobsByTitleAsync(Guid jobSeekerId, string title);
        Task<bool> RemoveSavedJobAsync(Guid jobSeekerId, Guid savedJobId);
        Task<IEnumerable<InterviewDto>> GetScheduledInterviewsAsync(Guid jobSeekerId);
        Task <JobPostDto?> GetJobByTitleAsync(string title);




        
    }
}
