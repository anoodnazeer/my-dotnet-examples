using JobPortalMVC.Models;

namespace JobPortalMVC.Interface
{
    public interface  IJobService
    {
        public List<Job> GetJobs();
        public Task GetJobSelectedAsync(int? id);
        Task ApplyJobAsync(int userId, int jobId);
    }
}
