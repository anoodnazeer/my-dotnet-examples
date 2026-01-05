using AutoMapper;
using Domain.Models;
using Domain.Service.Admin.DTOs;
using Domain.Service.Admin.Interfaces;
using Domain.Service.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.JobProvider.DTOs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service.Admin
{

    public class AdminService : IAdminServices
    {
        IAdminRepository _adminRepository;
        IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)


        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddSkillAsync(SkillDto skill)
        {
            var Skill = _mapper.Map<Skill>(skill);
            var result = await _adminRepository.AddAsync(Skill);

            return result;
        }
        public async Task<bool> UpdateSkillAsync(Guid skillId, SkillDto skill)
        {
            var updatedSkill = _mapper.Map<Skill>(skill);
            return await _adminRepository.UpdateAsync(skillId, updatedSkill);
        }

        public async Task<bool> PatchSkillAsync(Guid skillId, SkillDto skill)
        {
            var updatedSkill = _mapper.Map<Skill>(skill);
            return await _adminRepository.PatchAsync(skillId, updatedSkill);
        }
        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _adminRepository.GetAllSkillsAsync();
            return _mapper.Map<IEnumerable<SkillDto>>(skills);
        }
        public async Task<SkillDto?> GetSkillByIdAsync(Guid id)
        {
            var skill = await _adminRepository.GetSkillByIdAsync(id);
            return _mapper.Map<SkillDto>(skill);
        }
        public async Task<bool> RemoveSkillAsync(Guid skillId)
        {
            var result = await _adminRepository.RemoveAsync(skillId);

            return result;
        }


        //Add Industry
        public async Task<IndustryDto> AddIndustryAsync(IndustryDto request)
        {
            // Map Domain DTO → Entity
            var industry = _mapper.Map<Industry>(request);
            industry.Id = Guid.NewGuid(); // assign new Id

            _adminRepository.AddIndustry(industry);


            // Map Entity → Domain DTO
            return _mapper.Map<IndustryDto>(industry);
        }


        //Get Industry
        public async Task<List<IndustryDto>> GetAllIndustriesAsync()
        {
            var industries = await _adminRepository.GetAllIndustriesAsync();
            return _mapper.Map<List<IndustryDto>>(industries);
        }

        //Get IndustryById
        public async Task<IndustryDto> GetIndustryByIdAsync(Guid id)
        {
            var industry = await _adminRepository.GetIndustryByIdAsync(id);

            if (industry == null)
                return null;

            return _mapper.Map<IndustryDto>(industry);
        }

        //Get industryCount
        public async Task<int> GetIndustryCountAsync()
        {
            return await _adminRepository.GetIndustryCountAsync();
        }

        //Edit Industry
        public async Task<IndustryDto?> UpdateIndustryAsync(Guid id, IndustryDto dto)
        {
            var existing = await _adminRepository.GetIndustryByIdAsync(id);
            if (existing == null)
                return null;

            // Update fields
            existing.Name = dto.Name;
            existing.Description = dto.Description;

            var updated = await _adminRepository.UpdateIndustryAsync(existing);
            return _mapper.Map<IndustryDto>(updated);
        }

        //patch industry
        public async Task<Industry?> PatchIndustryAsync(Guid id, IndustryDto updatedData)
        {
            // Map DTO → Entity
            var mappedPatch = _mapper.Map<Industry>(updatedData);

            // Call repository
            var result = await _adminRepository.PatchIndustryAsync(id, mappedPatch);

            return result;
        }


        //Delete Industry
        public async Task<bool> DeleteIndustryAsync(Guid id)
        {
            return await _adminRepository.DeleteIndustryAsync(id);

        }


       
        public async Task<bool> AddLocationAsync(LocationDto location)
        {
            var locationEntity = _mapper.Map<Location>(location);
            var result = await _adminRepository.AddLocationAsync(locationEntity);
            return result;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _adminRepository.GetAllLocationsAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto?> GetLocationByIdAsync(Guid id)
        {
            var location = await _adminRepository.GetLocationByIdAsync(id);
            return _mapper.Map<LocationDto?>(location);
        }

        public async Task<bool> UpdateLocationAsync(Guid locationId, LocationDto location)
        {
            var updatedLocation = _mapper.Map<Location>(location);
            return await _adminRepository.UpdateLocationAsync(locationId, updatedLocation);
        }

        public async Task<bool> PatchLocationAsync(Guid locationId, LocationDto location)
        {
            var updatedLocation = _mapper.Map<Location>(location);
            return await _adminRepository.PatchLocationAsync(locationId, updatedLocation);
        }

        public async Task<bool> RemoveLocationAsync(Guid locationId)
        {
            return await _adminRepository.RemoveLocationAsync(locationId);
        }

        public async Task<JobCategoryDto> CreateJobCategoryAsync(JobCategoryDto dto)
        {
            dto.Id = Guid.NewGuid();
            var mappeddto = _mapper.Map<JobCategory>(dto);

            var created = await _adminRepository.AddJobCategoryAsync(mappeddto);

            return _mapper.Map<JobCategoryDto>(created);

        }

        //getAllJobcategory
        public async Task<IEnumerable<JobCategoryDto>> GetAllJobCategoryAsync()
        {
            var categories = await _adminRepository.GetAllJobCategoryAsync();
            return _mapper.Map<IEnumerable<JobCategoryDto>>(categories);
        }

        //GetJobCategoryById
        public async Task<JobCategoryDto?> GetJobCategoryByIdAsync(Guid id)
        {
            var category = await _adminRepository.GetJobCategoryByIdAsync(id);
            var categories = _mapper.Map<JobCategoryDto>(category);
            return categories;
        }

        //UpdateJobCategory
        public async Task<bool> UpdateJobCategoryAsync(Guid id, JobCategoryDto dto)
        {
            var existing = await _adminRepository.GetJobCategoryByIdAsync(id);

            if (existing == null) return false;

     existing.Name = dto.Name;
     existing.Description = dto.Description;

     return await _adminRepository.UpdateJobCategoryAsync(existing);
 }
        // patch JobserviceCategory
        public async Task<bool> PatchJobCategoryAsync(Guid id, JobCategoryDto dto)
        {
            var existing = await _adminRepository.GetJobCategoryByIdAsync(id);
            if (existing == null)
                return false;

            if (!string.IsNullOrEmpty(dto.Name) && dto.Name.ToLower() != "string")
                existing.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Description) && dto.Description.ToLower() != "string")
                existing.Description = dto.Description;

            return await _adminRepository.PatchJobCategoryAsync(existing);
        }

        //Delete JobCategory
        public async Task<bool> DeleteJobCategoryAsync(Guid id)
        {
            return await _adminRepository.DeleteJobCategoryAsync(id);
        }



        public async Task<IEnumerable<JobDto>> GetPendingJobsAsync()
        {
            var jobs = await _adminRepository.GetPendingJobsAsync();
            return _mapper.Map<IEnumerable<JobDto>>(jobs);
        }


        public async Task<int> GetJobCountAsync()
        {
            return await _adminRepository.GetJobCountAsync();
        }

        public async Task<JobPost?> GetJobByNameAsync(string jobTitle)
        {
            return await _adminRepository.GetJobByNameAsync(jobTitle);
        }


        public async Task<IEnumerable<JobProviderDto>> GetAllProviders()
        {
            var jobProviders = await _adminRepository.GetAllProviders(); // wait for the async result
            var mappedProviders = _mapper.Map<IEnumerable<JobProviderDto>>(jobProviders);
            return mappedProviders;
        }


        //Get GetjobproviderbyidById
        public async Task<JobProviderDto> GetJobProviderByIdAsync(Guid id)
        {
            var Jobprovider = await _adminRepository.GetJobProviderByIdAsync(id);

            if (Jobprovider == null)
                return null;

            return _mapper.Map<JobProviderDto>(Jobprovider);
        }

        //JobproviderCount
        public async Task<int> GetJobProviderCountAsync()
        {
            return await _adminRepository.GetJobProviderCountAsync();
        }


        //Delete JobProvider
        public async Task<bool> DeleteJobProviderAsync(Guid id)
        {
            return await _adminRepository.DeleteJobProviderAsync(id);
        }


        public async Task<bool> ApproveJobAsync(Guid jobId)
        {
            return await _adminRepository.ApproveJobAsync(jobId);
        }

        public async Task<bool> RejectJobAsync(Guid jobId)
        {
            return await _adminRepository.RejectJobAsync(jobId);
        }

    }

    }

