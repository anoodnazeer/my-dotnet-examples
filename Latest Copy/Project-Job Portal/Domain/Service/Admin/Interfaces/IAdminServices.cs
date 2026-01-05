
﻿using Domain.Models;
using Domain.Service.Admin.DTOs;
using Domain.Service.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Service.JobProvider.DTOs;


namespace Domain.Service.Admin.Interfaces
{
    public interface IAdminServices
    {

        Task<bool> AddSkillAsync(SkillDto skill);

        Task<bool> RemoveSkillAsync(Guid skillId);
        Task<bool> UpdateSkillAsync(Guid skillId, SkillDto skill);
        Task<bool> PatchSkillAsync(Guid skillId, SkillDto skill);
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
        Task<bool> AddLocationAsync(LocationDto location);
        Task<SkillDto?> GetSkillByIdAsync(Guid id);
        Task<bool> UpdateLocationAsync(Guid locationId, LocationDto location);
        Task<bool> PatchLocationAsync(Guid locationId, LocationDto location);

        Task<bool> RemoveLocationAsync(Guid locationId);
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto?> GetLocationByIdAsync(Guid id);
      
        Task<IndustryDto> AddIndustryAsync(IndustryDto request);
        Task<List<IndustryDto>> GetAllIndustriesAsync();
        Task<IndustryDto> GetIndustryByIdAsync(Guid id);

        Task<int> GetIndustryCountAsync();
        Task<IndustryDto?> UpdateIndustryAsync(Guid id, IndustryDto dto);
        Task<Industry?> PatchIndustryAsync(Guid id, IndustryDto updatedData);
        Task<bool> DeleteIndustryAsync(Guid id);

        Task<IEnumerable<JobDto>> GetPendingJobsAsync();

        Task<JobCategoryDto> CreateJobCategoryAsync(JobCategoryDto dto);
        Task<IEnumerable<JobCategoryDto>> GetAllJobCategoryAsync();
        Task<JobCategoryDto?> GetJobCategoryByIdAsync(Guid id);
        Task<bool> UpdateJobCategoryAsync(Guid id, JobCategoryDto dto);
        Task<bool> PatchJobCategoryAsync(Guid id, JobCategoryDto dto);
        Task<bool> DeleteJobCategoryAsync(Guid id);

        Task<int> GetJobCountAsync();
        Task<JobPost?> GetJobByNameAsync(string jobTitle);

        Task<IEnumerable<JobProviderDto>> GetAllProviders();
        Task<JobProviderDto> GetJobProviderByIdAsync(Guid id);

        Task<int> GetJobProviderCountAsync();

        Task<bool> DeleteJobProviderAsync(Guid id);
        Task<bool> ApproveJobAsync(Guid jobId);
        Task<bool> RejectJobAsync(Guid jobId);




    }
}
