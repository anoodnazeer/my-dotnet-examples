using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Service.JobProvider.Dto;
using Domain.Service.JobProvider.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service.JobProvider
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly HireMeNowDbContext _context;

        private readonly IMapper _mapper;

        public InterviewRepository(HireMeNowDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> ScheduleInterviewAsync(Interview interview)
        {
            // Validate related entities exist
            var jobPost = await _context.JobPosts.FindAsync(interview.JobId)
                ?? throw new Exception("Job not found");

            var jobSeeker = await _context.JobSeekers.FindAsync(interview.interviewee)
                ?? throw new Exception("JobSeeker not found");

            var company = await _context.JobProviderCompanies.FindAsync(jobPost.CompanyId)
                ?? throw new Exception("Company not found");

            // Assign CompanyId and new ID
            interview.CompanyId = jobPost.CompanyId;
            interview.Id = Guid.NewGuid();

            // ⚠️ Important: prevent EF from inserting related entities
            interview.Job = null;
            interview.Jobseeker = null;
            interview.Company = null;

            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();

            return interview.Id;
        }

        public async Task<List<InterviewDto>> GetAllScheduledInterviewsAsync()
        {
            var interviews = await _context.Interviews
                .Include(i => i.Job)
                .Include(i => i.Jobseeker)
                .Include(i => i.Company)
                .ToListAsync();

            return _mapper.Map<List<InterviewDto>>(interviews);
        }

        public async Task<Interview?> GetInterviewByIdAsync(Guid id)
        {
            return await _context.Interviews
                .Include(i => i.Job)
                .Include(i => i.Jobseeker)
                .Include(i => i.Company)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> UpdateInterviewAsync(Guid id, Interview updatedInterview)
        {
            var existing = await _context.Interviews.FindAsync(id);
            if (existing == null) return false;

            existing.DateScheduled = updatedInterview.DateScheduled;
            existing.Mode = updatedInterview.Mode;

            _context.Interviews.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchInterviewAsync(Guid id, string newTime)
        {
            // Use FirstOrDefaultAsync to reliably find the interview
            var existing = await _context.Interviews
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existing == null) return false; // triggers 404

            // Normalize time string (remove spaces like "A M" -> "AM")
            string normalizedTime = newTime.Replace(" ", "");

            // Define supported time formats
            string[] formats = { "h:mmtt", "hh:mmtt" }; // e.g., 11:30AM

            if (DateTime.TryParseExact(normalizedTime, formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime parsedTime))
            {
                // Combine existing date with new time
                existing.DateScheduled = new DateTime(
                    existing.DateScheduled.Year,
                    existing.DateScheduled.Month,
                    existing.DateScheduled.Day,
                    parsedTime.Hour,
                    parsedTime.Minute,
                    parsedTime.Second
                );

                await _context.SaveChangesAsync();
                return true;
            }

            return false; // invalid time format
        }

        public async Task<bool> UpdateInterviewStatusAsync(Guid id, string status)
        {
            var existing = await _context.Interviews.FindAsync(id);
            if (existing == null) return false;

            existing.Status = status;
            _context.Interviews.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInterviewAsync(Guid id)
        {
            var existing = await _context.Interviews.FindAsync(id);
            if (existing == null) return false;

            _context.Interviews.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
