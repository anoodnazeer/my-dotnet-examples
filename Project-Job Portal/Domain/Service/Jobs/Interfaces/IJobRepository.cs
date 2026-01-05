using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Jobs.Interfaces
{
    public interface IJobRepository
    {
        // 21. Create Job Post
        Task<Guid> CreateJobPostAsync(JobPost jobPost);

        // 22. Get Job By ID
        Task<JobPost?> GetJobByIdAsync(Guid id);

        // 23. Update Job By ID
        Task<bool> UpdateJobByIdAsync(Guid id, JobPost updatedJob);

        // 24. Patch Job By ID (partial update — e.g., salary only)
        Task<bool> PatchJobByIdAsync(Guid id, decimal? salary);

        // 25. Delete Job By ID
        Task<bool> DeleteJobByIdAsync(Guid id);

        // 26. Get All Jobs
        Task<List<JobPost>> GetAllJobsAsync();
    }
}
