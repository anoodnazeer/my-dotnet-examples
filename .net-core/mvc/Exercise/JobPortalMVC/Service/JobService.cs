using JobPortalMVC.Interface;
using JobPortalMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortalMVC.Service
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public List<Job> GetJobs()
        {
            return _jobRepository.GetJobs();
        }
        public Task GetJobSelectedAsync<IActionResult>(int? id);
        {
            return _jobRepository.(jobid);
        }

        //public async Task ApplyJobAsync(int userId, int jobId)
        //{
        //    var applied = new Application
        //    {
        //        JobId = jobId,
        //        UserId = userId,
        //        AppliedDate = DateTime.UtcNow
        //    };
        //    await _jobRepository.AddAsync(applied);
        //    await _jobRepository.SaveAsync();
        //}

        
    }
}
