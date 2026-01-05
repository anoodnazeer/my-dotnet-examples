using JobPortalMVC.Interface;
using JobPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortalMVC.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Application applied)
        {
            throw new NotImplementedException();
        }

        public List<Job> GetJobs()
        {
            return _context.Jobs.ToList();
        }

        public async Task<Job> GetJobSelectedAsync(int? id)
        {
            if (id == null) return null;
            return await _context.Jobs.FindAsync(id.Value);
        }

         
    }
}
