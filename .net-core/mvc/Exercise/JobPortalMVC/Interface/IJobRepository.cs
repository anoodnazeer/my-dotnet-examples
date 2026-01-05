using JobPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalMVC.Interface
{
    public interface  IJobRepository
    {
        Task AddAsync(Application applied);
        public List<Job> GetJobs();
        public Task GetJobSelectedAsync(int? id);


    }
}
