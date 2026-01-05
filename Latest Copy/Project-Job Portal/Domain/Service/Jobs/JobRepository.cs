using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Service.Jobs.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service.Jobs
{
    public class JobRepository : IJobRepository
    {
        private readonly HireMeNowDbContext _context;
        private readonly IMapper _mapper;

        public JobRepository(HireMeNowDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // -------------------------
        // JOB POST METHODS
        // -------------------------

        public async Task<Guid> CreateJobPostAsync(JobPost jobPost)
        {
            jobPost.Id = Guid.NewGuid();
            jobPost.PostedDate = DateTime.UtcNow;

            await _context.JobPosts.AddAsync(jobPost);
            await _context.SaveChangesAsync();

            return jobPost.Id;
        }

        public async Task<JobPost?> GetJobByIdAsync(Guid id)
        {
            return await _context.JobPosts
                                 .Include(jp => jp.Company)
                                 .Include(jp => jp.Location)
                                 .Include(jp => jp.Category)
                                 .Include(jp => jp.Industry)
                                 .Include(jp => jp.JobResponsibilities)
                                 .FirstOrDefaultAsync(jp => jp.Id == id);
        }

        public async Task<bool> UpdateJobByIdAsync(Guid id, JobPost updatedJob)
        {
            var existingJob = await _context.JobPosts.FindAsync(id);
            if (existingJob == null) return false;

            // Only update fields present in updatedJob
            if (!string.IsNullOrEmpty(updatedJob.JobTitle))
                existingJob.JobTitle = updatedJob.JobTitle;

            if (!string.IsNullOrEmpty(updatedJob.JobSummary))
                existingJob.JobSummary = updatedJob.JobSummary;

            if (updatedJob.LocationId != Guid.Empty)
                existingJob.LocationId = updatedJob.LocationId;

            if (updatedJob.IndustryId != Guid.Empty)
                existingJob.IndustryId = updatedJob.IndustryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchJobByIdAsync(Guid id, decimal? salary)
        {
            var existingJob = await _context.JobPosts.FindAsync(id);
            if (existingJob == null) return false;

            if (salary.HasValue)
                existingJob.Salary = salary.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteJobByIdAsync(Guid id)
        {
            var existingJob = await _context.JobPosts.FindAsync(id);
            if (existingJob == null) return false;

            _context.JobPosts.Remove(existingJob);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<JobPost>> GetAllJobsAsync()
        {
            return await _context.JobPosts
                                 .Include(jp => jp.Company)
                                 .Include(jp => jp.Location)
                                 .Include(jp => jp.Industry)
                                 .ToListAsync();
        }
    }
}
