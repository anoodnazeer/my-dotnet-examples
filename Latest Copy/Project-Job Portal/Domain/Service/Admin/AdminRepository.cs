using AutoMapper;
using Domain.Models;
using Domain.Service.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Service.Admin.DTOs;


namespace Domain.Service.Admin
{
    public class AdminRepository : IAdminRepository
    {

        private readonly List<Domain.Models.JobSeeker> _jobSeeker;
        HireMeNowDbContext _context;
        IMapper _mapper;

        public AdminRepository(HireMeNowDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(Skill skill)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));
            if (_context.Skills.Any(s => s.Name == skill.Name))
            {
                return false; // Skill with the same name already exists
            }
            skill.Id = Guid.NewGuid();
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return true; // Skill added successfully
        }
        public async Task<bool> UpdateAsync(Guid skillId, Skill updatedSkill)
        {
            var existingSkill = await _context.Skills.FindAsync(skillId);
            if (existingSkill == null) return false;

            // Replace all fields
            existingSkill.Name = updatedSkill.Name;
            existingSkill.Description = updatedSkill.Description;

            _context.Skills.Update(existingSkill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(Guid skillId, Skill updatedSkill)
        {
            var existingSkill = await _context.Skills.FindAsync(skillId);
            if (existingSkill == null) return false;

            // Update only provided fields
            if (!string.IsNullOrEmpty(updatedSkill.Name))
                existingSkill.Name = updatedSkill.Name;

            if (!string.IsNullOrEmpty(updatedSkill.Description))
                existingSkill.Description = updatedSkill.Description;

            _context.Skills.Update(existingSkill);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }
        public async Task<Skill?> GetSkillByIdAsync(Guid id)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> RemoveAsync(Guid skillId)
        {
            var skillToRemove = await _context.Skills.FindAsync(skillId);

            if (skillToRemove == null)
            {
                return false; // Skill not found
            }

            _context.Skills.Remove(skillToRemove);
            await _context.SaveChangesAsync();

            return true; // Skill removed successfully
        }
        public async Task<bool> AddLocationAsync(Location location)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));

            // Check if location with same name already exists
            if (_context.Locations.Any(l => l.Name == location.Name))
                return false;

            location.Id = Guid.NewGuid();
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(Guid id)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> UpdateLocationAsync(Guid locationId, Location updatedLocation)
        {
            var existingLocation = await _context.Locations.FindAsync(locationId);
            if (existingLocation == null) return false;

            existingLocation.Name = updatedLocation.Name;
            existingLocation.Description = updatedLocation.Description;
            existingLocation.City = updatedLocation.City;
            existingLocation.State = updatedLocation.State;
            existingLocation.Country = updatedLocation.Country;

            _context.Locations.Update(existingLocation);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PatchLocationAsync(Guid locationId, Location updatedLocation)
        {
            var existingLocation = await _context.Locations.FindAsync(locationId);
            if (existingLocation == null) return false;

            // Update only provided fields
            if (!string.IsNullOrEmpty(updatedLocation.Name))
                existingLocation.Name = updatedLocation.Name;

            if (!string.IsNullOrEmpty(updatedLocation.Description))
                existingLocation.Description = updatedLocation.Description;
            if (!string.IsNullOrEmpty(updatedLocation.Description))
                existingLocation.State = updatedLocation.State;
            if (!string.IsNullOrEmpty(updatedLocation.Description))
                existingLocation.Country = updatedLocation.Country;
            if (!string.IsNullOrEmpty(updatedLocation.Description))
                existingLocation.City = updatedLocation.City;

            _context.Locations.Update(existingLocation);
            await _context.SaveChangesAsync();
            return true;
        }

        //Add Industry
        public void AddIndustry(Industry industry)
        {
            _context.Industries.Add(industry);
            _context.SaveChangesAsync();
        }
        //Get Industry

        public async Task<List<Industry>> GetAllIndustriesAsync()
        {
            return await _context.Industries.ToListAsync();
        }


        public async Task<Industry?> GetIndustryByIdAsync(Guid id)
        {
            return await _context.Industries.FindAsync(id);
            
        }

        //get IndustryCount
        public async Task<int> GetIndustryCountAsync()
        {
            return await _context.Industries.CountAsync();
        }


        //Edit Industry
        public async Task<Industry> UpdateIndustryAsync(Industry industry)
        {
            _context.Industries.Update(industry);
            await _context.SaveChangesAsync();
            return industry;
        }

        //patch Industry
        public async Task<Industry?> PatchIndustryAsync(Guid id, Industry updatedData)
        {
            var existing = await _context.Industries.FindAsync(id);
            if (existing == null)
                return null;

            // ✅ Update only if values are provided in request
            if (!string.IsNullOrWhiteSpace(updatedData.Name))
                existing.Name = updatedData.Name;

            if (!string.IsNullOrWhiteSpace(updatedData.Description))
                existing.Description = updatedData.Description;

            await _context.SaveChangesAsync();

            // ✅ Return the latest saved entity (with existing or updated values)
            return existing;
        }

        //Delete Industry
        public async Task<bool> DeleteIndustryAsync(Guid id)
        {
            var existing = await _context.Industries.FindAsync(id);
            if (existing == null)
                return false;

            _context.Industries.Remove(existing);

            await _context.SaveChangesAsync();
            return true;
        }


       
        public async Task<IEnumerable<JobPost>> GetPendingJobsAsync()
        {
            return await _context.JobPosts
                .Where(j => j.Status == "Pending")
                .ToListAsync();
        }
        public async Task<bool> RemoveLocationAsync(Guid locationId)
        {
            var locationToRemove = await _context.Locations.FindAsync(locationId);
            if (locationToRemove == null) return false;

            _context.Locations.Remove(locationToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<JobCategory> AddJobCategoryAsync(JobCategory category)
        {
            _context.JobCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }


        //GetAllCategory
        public async Task<IEnumerable<JobCategory>> GetAllJobCategoryAsync()
        {
            return await _context.JobCategories.ToListAsync();
        }

        //GetCategoryById
        public async Task<JobCategory?> GetJobCategoryByIdAsync(Guid id)
        {
            return await _context.JobCategories.FindAsync(id);
        }

        //updateJobCategory
        public async Task<bool> UpdateJobCategoryAsync(JobCategory category)
        {
            _context.JobCategories.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }

        //PatchJobCategory
        public async Task<bool> PatchJobCategoryAsync(JobCategory category)
        {


            _context.JobCategories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        //Delete JobCategory
        public async Task<bool> DeleteJobCategoryAsync(Guid id)
        {
            var existing = await _context.JobCategories.FindAsync(id);
            if (existing == null)
                return false;

            _context.JobCategories.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> GetJobCountAsync()
        {
            return await _context.JobPosts.CountAsync();
        }

        public async Task<JobPost?> GetJobByNameAsync(string jobTitle)
        {


            var Job = await _context.JobPosts.FirstOrDefaultAsync(x => x.JobTitle == jobTitle);
            return Job;
        }


        public async Task<IEnumerable<JobProviderCompany>> GetAllProviders()
        {
            return await _context.JobProviderCompanies.ToListAsync();
        }
        public async Task<JobProviderCompany?> GetJobProviderByIdAsync(Guid id)
        {
            return await _context.JobProviderCompanies.FindAsync(id);

        }

        public async Task<int> GetJobProviderCountAsync()
        {
            return await _context.JobProviderCompanies.CountAsync();
        }
        public async Task<bool> DeleteJobProviderAsync(Guid id)
        {
            var existing = await _context.JobProviderCompanies.FindAsync(id);
            if (existing == null)
                return false;

            _context.JobProviderCompanies.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ApproveJobAsync(Guid jobId)
        {
            var job = await _context.JobPosts.FindAsync(jobId);
            if (job == null) return false;

            job.Status = "Approved";
            _context.JobPosts.Update(job);

            await _context.SaveChangesAsync();
            return true;
        }


      
      

        public async Task<bool> RejectJobAsync(Guid jobId)
        {
            var job = await _context.JobPosts.FindAsync(jobId);
            if (job == null) return false;

            job.Status = "Rejected";
            _context.JobPosts.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
