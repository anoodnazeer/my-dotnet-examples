
﻿using Domain.Models;
using System;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Admin.Interfaces
{
    public interface IAdminRepository
    {

        Task<bool> AddAsync(Skill skill);

        Task<bool> RemoveAsync(Guid skillId);
        Task<bool> UpdateAsync(Guid skillId, Skill updatedSkill);
        Task<bool> PatchAsync(Guid skillId, Skill updatedSkill);
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<bool> AddLocationAsync(Location location);
        Task<Skill?> GetSkillByIdAsync(Guid id);
        Task<bool> UpdateLocationAsync(Guid locationId, Location updatedLocation);
        Task<bool> PatchLocationAsync(Guid locationId, Location updatedLocation);

        Task<bool> RemoveLocationAsync(Guid locationId);
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(Guid id);
       

        void AddIndustry(Industry industry);
        Task<List<Industry>> GetAllIndustriesAsync();
        Task<Industry?> GetIndustryByIdAsync(Guid id);
        Task<int> GetIndustryCountAsync();
        Task<Industry> UpdateIndustryAsync(Industry industry);
        Task<Industry?> PatchIndustryAsync(Guid id, Industry industry);
        Task<bool> DeleteIndustryAsync(Guid id);


        Task<IEnumerable<JobPost>> GetPendingJobsAsync();

        Task<JobCategory> AddJobCategoryAsync(JobCategory category);
        Task<IEnumerable<JobCategory>> GetAllJobCategoryAsync();
        Task<JobCategory?> GetJobCategoryByIdAsync(Guid id);
        Task<bool> UpdateJobCategoryAsync(JobCategory category);
        Task<bool> PatchJobCategoryAsync(JobCategory category);
        Task<bool> DeleteJobCategoryAsync(Guid id);
        Task<int> GetJobCountAsync();
        Task<JobPost?> GetJobByNameAsync(string jobTitle);

        Task<IEnumerable<JobProviderCompany>> GetAllProviders();
        Task<JobProviderCompany?> GetJobProviderByIdAsync(Guid id);
        Task<int> GetJobProviderCountAsync();

        Task<bool> DeleteJobProviderAsync(Guid id);
        Task<bool> ApproveJobAsync(Guid jobId);
        Task<bool> RejectJobAsync(Guid jobId);



    }
}
