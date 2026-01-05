using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Service.JobSeeker.Interfaces
{
    public interface IJobSeekerRepository
    {
        Guid AddSignupRequest(SignUpRequest signUpRequest);
        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobSeekerSignupRequestId);
        void UpdateSignupRequest(SignUpRequest signUpRequest);
        Task AddJobSeekerAsync(Models.JobSeeker jobseeker);
        Task<bool> CreateJobApplicationAsync(JobApplication jobApplication);
        Task<bool> HasAlreadyAppliedAsync(Guid jobSeekerId, Guid jobPostId);
        Task<List<JobApplication>> GetAppliedJobsAsync(Guid jobSeekerId);
        Task<List<JobApplication>> GetAppliedJobsByTitleAsync(Guid jobSeekerId, string jobTitle);
        Task<bool> CancelAppliedJobAsync(Guid jobSeekerId, Guid jobApplicationId);
        Task<bool> SaveJobAsync(SavedJob savedJob);
        Task<bool> JobExistsAsync(Guid jobId);
        Task<List<SavedJob>> GetSavedJobsAsync(Guid jobSeekerId);
        Task<List<SavedJob>> GetSavedJobsByTitleAsync(Guid jobSeekerId, string title);
        Task<bool> RemoveSavedJobAsync(Guid jobSeekerId, Guid savedJobId);
        Task<IEnumerable<Interview>> GetAllByJobSeekerIdAsync(Guid jobSeekerId);
        Task<JobSeekerProfile> GetByIdAsync(Guid id);
        Task<IEnumerable<JobSeekerProfile>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
        Task<int> GetCountAsync();




    }
}
