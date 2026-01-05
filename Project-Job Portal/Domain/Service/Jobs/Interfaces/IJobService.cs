using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.Jobs.Dto;

namespace Domain.Service.Jobs.Interfaces
{
    public interface IJobService
    {
        // -------------------------
        // JOB POST METHODS
        // -------------------------
        Task<Guid> CreateJobPostAsync(JobPostDto jobPost);
        Task<JobPostDto?> GetJobByIdAsync(Guid id);
        Task<bool> UpdateJobByIdAsync(Guid id, JobPostDto updatedJob);
        Task<bool> PatchJobByIdAsync(Guid id, decimal? salary);
        Task<bool> DeleteJobByIdAsync(Guid id);
        Task<List<JobPostDto>> GetAllJobsAsync();
    }
}
