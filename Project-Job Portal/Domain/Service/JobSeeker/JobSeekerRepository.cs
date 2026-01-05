using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Domain.Service.JobSeeker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service.JobSeeker
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly HireMeNowDbContext _context;

        public JobSeekerRepository(HireMeNowDbContext dbContext)
        {
            _context = dbContext;
        }
        public Guid AddSignupRequest(SignUpRequest signUpRequest)
        {
            signUpRequest.Status = Status.PENDING;
            _context.SignUpRequests.AddAsync(signUpRequest);
            _context.SaveChanges();
            return signUpRequest.Id;
        }

        public async Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobSeekerSignupRequestId)
        {
            return await _context.SignUpRequests.FindAsync(jobSeekerSignupRequestId);
        }

        public void UpdateSignupRequest(SignUpRequest signUpRequest)
        {
            _context.SignUpRequests.Update(signUpRequest);
            _context.SaveChanges();
        }

        public async Task AddJobSeekerAsync(Models.JobSeeker jobseeker)
        {
            await _context.JobSeekers.AddAsync(jobseeker);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> CreateJobApplicationAsync(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HasAlreadyAppliedAsync(Guid jobSeekerId, Guid jobPostId)
        {
            return await _context.JobApplications
                .AnyAsync(a => a.ApplicantId == jobSeekerId && a.JobPostId == jobPostId);
        }

        public async Task<List<JobApplication>> GetAppliedJobsAsync(Guid jobSeekerId)
        {
            return await _context.JobApplications
                .Include(j => j.JobPost)
                .Where(j => j.ApplicantId == jobSeekerId)
                .ToListAsync();
        }

        public async Task<List<JobApplication>> GetAppliedJobsByTitleAsync(Guid jobSeekerId, string jobTitle)
        {
            return await _context.JobApplications
                .Include(j => j.JobPost)
                .Where(j => j.ApplicantId == jobSeekerId &&
                            j.JobPost.JobTitle.Contains(jobTitle))
                .ToListAsync();
        }

        public async Task<bool> CancelAppliedJobAsync(Guid jobSeekerId, Guid jobApplicationId)
        {
            try
            {
                var appliedJob = await _context.JobApplications
                    .FirstOrDefaultAsync(j => j.Id == jobApplicationId && j.ApplicantId == jobSeekerId);

                if (appliedJob == null)
                    return false;

                _context.JobApplications.Remove(appliedJob);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> JobExistsAsync(Guid jobId)
        {
            return await _context.JobPosts.AnyAsync(j => j.Id == jobId);
        }

        public async Task<bool> SaveJobAsync(SavedJob savedJob)
        {
            await _context.SavedJobs.AddAsync(savedJob);
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<List<SavedJob>> GetSavedJobsAsync(Guid jobSeekerId)
        {
            return await _context.SavedJobs
                .Include(s => s.JobPost)
                .Where(s => s.SavedBy == jobSeekerId)
                .ToListAsync();
        }

        public async Task<List<SavedJob>> GetSavedJobsByTitleAsync(Guid jobSeekerId, string title)
        {
            return await _context.SavedJobs
                .Include(s => s.JobPost)
                .Where(s => s.SavedBy == jobSeekerId && s.JobPost.JobTitle.Contains(title))
                .ToListAsync();
        }

        public async Task<bool> RemoveSavedJobAsync(Guid jobSeekerId, Guid savedJobId)
        {
            var entity = await _context.SavedJobs
                .FirstOrDefaultAsync(s => s.SavedBy == jobSeekerId && s.Id == savedJobId);
            if (entity != null)
            {
                _context.SavedJobs.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Interview>> GetAllByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _context.Interviews
                .Include(i => i.Job)
                .Include(i => i.Company)
                .Where(i => i.interviewee == jobSeekerId)
                .ToListAsync();
        }

        //GetJobseekers
        // ✅ Get JobSeekerProfile by Id (with related JobSeeker info)
        public async Task<JobSeekerProfile> GetByIdAsync(Guid id)
        {
            return await _context.JobSeekerProfiles
      .Include(p => p.JobSeeker) // keep this if JobSeeker exists
      .FirstOrDefaultAsync(p => p.Id == id);
        }

        // ✅ Get all JobSeekerProfiles (with related JobSeeker info)
        public async Task<IEnumerable<JobSeekerProfile>> GetAllAsync()
        {
            return await _context.JobSeekerProfiles.ToListAsync();
        }

        // ✅ Delete a JobSeekerProfile by Id
        public async Task<bool> DeleteAsync(Guid id)
        {
            var profile = await _context.JobSeekerProfiles.FindAsync(id);
            if (profile == null)
                return false;

            _context.JobSeekerProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Get total JobSeekerProfiles count
        public async Task<int> GetCountAsync()
        {
            return await _context.JobSeekerProfiles.CountAsync();
        }
    }
}
