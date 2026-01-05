using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;

using Domain.Service.Jobs.Dto;
using Domain.Service.Jobs.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Service.Jobs
{
    public class JobService : IJobService
    {

        private readonly IJobRepository _repository;
        private readonly HireMeNowDbContext _context; // add this
        private readonly IMapper _mapper;

        public JobService(IJobRepository repository, HireMeNowDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }
        // -------------------------
        // JOB POST METHODS
        // -------------------------

        public async Task<Guid> CreateJobPostAsync(JobPostDto jobPostDto)
        {
            // Fetch existing related entities
            var location = await _context.Locations.FindAsync(jobPostDto.LocationId);
            var industry = await _context.Industries.FindAsync(jobPostDto.IndustryId);
            var company = await _context.JobProviderCompanies.FindAsync(jobPostDto.CompanyId);
            var user = await _context.CompanyUsers.FindAsync(jobPostDto.PostedBy);
            var category = await _context.JobCategories.FindAsync(jobPostDto.CategoryId);

            // Throw exceptions if any required entity is missing
            if (location == null) throw new Exception("Location not found");
            if (industry == null) throw new Exception("Industry not found");
            if (company == null) throw new Exception("Company not found");
            if (user == null) throw new Exception("User not found");
            if (category == null) throw new Exception("Job Category not found"); // ✅ Added

            // ✅ Manual mapping to avoid AutoMapper null errors
            var jobPost = new JobPost
            {
                Id = Guid.NewGuid(),
                JobTitle = jobPostDto.JobTitle ?? "Untitled Job",
                JobSummary = jobPostDto.JobSummary ?? "No summary provided",
                PostedDate = DateTime.UtcNow,
                Salary = jobPostDto.Salary ?? 0,
                Experience = jobPostDto.Experience ?? "Not specified",
                ApplicationDeadline = jobPostDto.ApplicationDeadline ?? DateTime.UtcNow.AddDays(30),
                JobType = jobPostDto.JobType ?? "Full-Time",
                LocationId = jobPostDto.LocationId,
                IndustryId = jobPostDto.IndustryId,
                CompanyId = jobPostDto.CompanyId,
                PostedBy = jobPostDto.PostedBy,
                CategoryId = jobPostDto.CategoryId // ✅ Added
            };

            // ✅ Attach navigation entities
            jobPost.Location = location;
            jobPost.Industry = industry;
            jobPost.Company = company;
            jobPost.PostedByNavigation = user;
            jobPost.Category = category; // ✅ Added

            // ✅ Save to DB
            await _repository.CreateJobPostAsync(jobPost);

            return jobPost.Id;
        }


        public async Task<JobPostDto?> GetJobByIdAsync(Guid id)
        {
            var jobPost = await _repository.GetJobByIdAsync(id);
            return _mapper.Map<JobPostDto?>(jobPost);
        }

        public async Task<bool> UpdateJobByIdAsync(Guid id, JobPostDto updatedJobDto)
        {
            var updatedJob = _mapper.Map<JobPost>(updatedJobDto);
            return await _repository.UpdateJobByIdAsync(id, updatedJob);
        }

        public async Task<bool> PatchJobByIdAsync(Guid id, decimal? salary)
        {
            return await _repository.PatchJobByIdAsync(id, salary);
        }

        public async Task<bool> DeleteJobByIdAsync(Guid id)
        {
            return await _repository.DeleteJobByIdAsync(id);
        }

        public async Task<List<JobPostDto>> GetAllJobsAsync()
        {
            var jobs = await _repository.GetAllJobsAsync();
            return _mapper.Map<List<JobPostDto>>(jobs);
        }

    }
}
