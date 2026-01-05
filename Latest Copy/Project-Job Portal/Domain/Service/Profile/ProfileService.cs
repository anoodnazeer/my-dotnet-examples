using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Service.JobSeeker;
using Domain.Service.Profile.DTOs;
using Domain.Service.Profile.Interface;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Service.Profile
{
    public class ProfileService : IJobSeekerProfileService
    {
        public readonly IJobSeekerProfileRepository _profileRepository;
        private readonly HireMeNowDbContext _context;
        IMapper mapper;
        public ProfileService(IJobSeekerProfileRepository profileRepository, IMapper _mapper, HireMeNowDbContext context)
        {
            mapper = _mapper;
            _profileRepository = profileRepository;
            _context = context;
        }

       
        public async Task<JobSeekerProfileDto> CreateProfileAsync(JobSeekerProfileDto dto, Guid jobSeekerId)
        {
           
            var jobSeeker = await _context.JobSeekers.FindAsync(jobSeekerId);
            if (jobSeeker == null)
                throw new InvalidOperationException("JobSeeker does not exist.");

            //// Check if profile already exists
            //bool profileExists = await _profileRepository.JobSeekerProfileExistsAsync(jobSeekerId);
            //if (profileExists)
            //    throw new InvalidOperationException("Profile already exists for this job seeker.");

            // Convert files to byte[]
            byte[]? imageData = null, resumeData = null;
            if (dto.SeekerImage != null)
            {
                using var ms = new MemoryStream();
                await dto.SeekerImage.CopyToAsync(ms);
                imageData = ms.ToArray();
            }

            if (dto.Resume != null)
            {
                using var ms = new MemoryStream();
                await dto.Resume.CopyToAsync(ms);
                resumeData = ms.ToArray();
            }

            var profile = new JobSeekerProfile
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                ProfileName = dto.ProfileName,
                ProfileSummary = dto.ProfileSummary,
                SeekerImage = imageData ?? Array.Empty<byte>(),
                Resume = resumeData ?? Array.Empty<byte>()
            };

            await _profileRepository.CreateJobseekerProfileAsync(profile);

            return new JobSeekerProfileDto
            {
                Id = profile.Id,
                JobSeekerId = profile.JobSeekerId,
                ProfileName = profile.ProfileName,
                ProfileSummary = profile.ProfileSummary
            };
        }






        public async Task<JobSeekerProfileDto> UpdateProfileAsync(JobSeekerProfileDto JobSeekerProfileDto, Guid jobSeekerId)
        {
            var profile = await _profileRepository.GetByJobSeekerIdAsync(jobSeekerId);

            if (profile == null)
                throw new Exception("Profile not found for this Job Seeker.");


            if (!string.IsNullOrEmpty(JobSeekerProfileDto.ProfileName))
                profile.ProfileName = JobSeekerProfileDto.ProfileName;

            if (!string.IsNullOrEmpty(JobSeekerProfileDto.ProfileSummary))
                profile.ProfileSummary = JobSeekerProfileDto.ProfileSummary;

            if (JobSeekerProfileDto.SeekerImage != null)
            {
                using var ms = new MemoryStream();
                await JobSeekerProfileDto.SeekerImage.CopyToAsync(ms);
                profile.SeekerImage = ms.ToArray();
            }

            if (JobSeekerProfileDto.Resume != null)
            {
                using var ms = new MemoryStream();
                await JobSeekerProfileDto.Resume.CopyToAsync(ms);
                profile.Resume = ms.ToArray();
            }

            var updated = await _profileRepository.UpdateJobseekerProfileAsync(profile);

            return new JobSeekerProfileDto
            {
                Id = updated.Id,
                JobSeekerId = updated.JobSeekerId,
                ProfileName = updated.ProfileName,
                ProfileSummary = updated.ProfileSummary
            };
        }


        public async Task<JobSeekerProfileDto?> PatchProfileAsync(JobSeekerProfileDto JobSeekerProfileDto, Guid jobSeekerId)
        {
            var profile = await _profileRepository.GetByJobSeekerIdAsync(jobSeekerId);

            if (profile == null)
                return null;

            if (!string.IsNullOrEmpty(JobSeekerProfileDto.ProfileName))
                profile.ProfileName = JobSeekerProfileDto.ProfileName;

            if (!string.IsNullOrEmpty(JobSeekerProfileDto.ProfileSummary))
                profile.ProfileSummary = JobSeekerProfileDto.ProfileSummary;

            if (JobSeekerProfileDto.SeekerImage != null)
            {
                using var ms = new MemoryStream();
                await JobSeekerProfileDto.SeekerImage.CopyToAsync(ms);
                profile.SeekerImage = ms.ToArray();
            }

            if (JobSeekerProfileDto.Resume != null)
            {
                using var ms = new MemoryStream();
                await JobSeekerProfileDto.Resume.CopyToAsync(ms);
                profile.Resume = ms.ToArray();
            }

            var updated = await _profileRepository.UpdateJobseekerProfileAsync(profile);

            return new JobSeekerProfileDto
            {
                Id = updated.Id,
                JobSeekerId = updated.JobSeekerId,
                ProfileName = updated.ProfileName,
                ProfileSummary = updated.ProfileSummary
            };
        }

        public async Task<JobSeekerProfileViewDto?> GetProfileByJobSeekerIdAsync(Guid jobSeekerId)
        {
            var profile = await _profileRepository.GetByJobSeekerIdAsync(jobSeekerId);

            if (profile == null)
                return null;

            return new JobSeekerProfileViewDto
            {
                Id = profile.Id,
                JobSeekerId = profile.JobSeekerId,
                ProfileName = profile.ProfileName,
                ProfileSummary = profile.ProfileSummary,
                ImageBase64 = profile.SeekerImage != null ? Convert.ToBase64String(profile.SeekerImage) : null,
                ResumeBase64 = profile.Resume != null ? Convert.ToBase64String(profile.Resume) : null
            };
        }


        public async Task<string> DeleteProfileAsync(Guid jobSeekerId)
        {

            var hasApplied = await _profileRepository.HasAppliedJobsAsync(jobSeekerId);
            if (hasApplied)
                return "Cannot delete profile. JobSeeker has applied for one or more jobs.";


            var deleted = await _profileRepository.DeleteProfileAsync(jobSeekerId);
            if (!deleted)
                return "Profile not found.";

            return "Profile deleted successfully.";
        }

        public async Task<byte[]?> GetResumeByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _profileRepository.GetResumeByJobSeekerIdAsync(jobSeekerId);
        }


        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _profileRepository.GetAllSkillsAsync();
        }

        public async Task<string> AddSkillsToJobSeekerAsync(Guid jobSeekerId, List<Guid> skillIds)
        {
            var success = await _profileRepository.AddSkillsToJobSeekerAsync(jobSeekerId, skillIds);
            return success ? "Skills added successfully." : "JobSeeker profile not found.";
        }


        public async Task<bool> UpdateSkillsAsync(Guid jobSeekerId, List<Guid> newSkillIds)
        {
            return await _profileRepository.UpdateSkillsAsync(jobSeekerId, newSkillIds);
        }


        public async Task<bool> PatchSkillsAsync(Guid jobSeekerId, List<Guid> addSkillIds, List<Guid> removeSkillIds)
        {
            return await _profileRepository.PatchSkillsAsync(jobSeekerId, addSkillIds, removeSkillIds);
        }

        public async Task<bool> DeleteSkillsAsync(Guid jobSeekerId, List<Guid> skillIds)
        {
            return await _profileRepository.DeleteSkillsAsync(jobSeekerId, skillIds);
        }

        public async Task<List<SkillDto>> GetSkillsByJobSeekerIdAsync(Guid jobSeekerId)
        {
            var skills = await _profileRepository.GetSkillsByJobSeekerIdAsync(jobSeekerId);
            return mapper.Map<List<SkillDto>>(skills);
        }

        public async Task<WorkExperienceDto> AddWorkExperienceAsync(Guid jobSeekerId, WorkExperienceDto dto)
        {

            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                throw new Exception("JobSeeker profile not found");

            var entity = mapper.Map<WorkExperience>(dto);
            entity.JobSeekerProfileId = profile.Id;
            entity.Id = Guid.NewGuid();

            await _profileRepository.AddWorkExperiencesync(entity);
            await _profileRepository.SaveChangesAsync();

            return mapper.Map<WorkExperienceDto>(entity);
        }

        public async Task<WorkExperienceDto> UpdateWorkExperienceAsync(Guid jobSeekerId, WorkExperienceDto dto)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                throw new Exception("JobSeeker profile not found");

            var existing = await _profileRepository.GetWorkExperienceByIdAsync(dto.Id);

            if (existing == null)
                throw new Exception("Work experience not found");


            if (existing.JobSeekerProfileId != profile.Id)
                throw new UnauthorizedAccessException("You can update only your own experiences.");


            existing.JobTitle = dto.JobTitle;
            existing.CompanyName = dto.CompanyName;
            existing.Summary = dto.Summary;
            existing.ServiceStart = dto.ServiceStart;
            existing.ServiceEnd = dto.ServiceEnd;

            await _profileRepository.UpdateWorkExperienceAsync(existing);

            return mapper.Map<WorkExperienceDto>(existing);
        }


        public async Task<WorkExperienceDto> PatchWorkExperienceAsync(Guid jobSeekerId, WorkExperienceDto dto)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                throw new Exception("JobSeeker profile not found.");

            var existing = await _profileRepository.GetWorkExperienceByIdAsync(dto.Id);

            if (existing == null)
                throw new Exception("Work experience not found.");

            if (existing.JobSeekerProfileId != profile.Id)
                throw new UnauthorizedAccessException("You can only modify your own work experiences.");


            if (!string.IsNullOrEmpty(dto.JobTitle)) existing.JobTitle = dto.JobTitle;
            if (!string.IsNullOrEmpty(dto.CompanyName)) existing.CompanyName = dto.CompanyName;
            if (!string.IsNullOrEmpty(dto.Summary)) existing.Summary = dto.Summary;
            if (dto.ServiceStart != default) existing.ServiceStart = dto.ServiceStart;
            if (dto.ServiceEnd != default) existing.ServiceEnd = dto.ServiceEnd;

            await _profileRepository.UpdateWorkExperienceAsync(existing);

            return mapper.Map<WorkExperienceDto>(existing);
        }

        public async Task<List<WorkExperienceDto>> GetWorkExperienceByJobSeekerIdAsync(Guid jobSeekerId)
        {
            var experiences = await _profileRepository.GetBySeekerIdAsync(jobSeekerId);

            if (experiences == null || !experiences.Any())
                return new List<WorkExperienceDto>();

            return mapper.Map<List<WorkExperienceDto>>(experiences);
        }

        public async Task<bool> DeleteWorkExperienceAsync(Guid workExperienceId, Guid jobSeekerId)
        {
            var existing = await _profileRepository.GetBySeekerWorkExperienceIdAsync(workExperienceId);

            if (existing == null)
                throw new KeyNotFoundException("Work experience not found.");


            if (existing.JobSeekerProfile.JobSeekerId != jobSeekerId)
                throw new UnauthorizedAccessException("You cannot delete another user's record.");

            return await _profileRepository.DeleteWorkExperienceAsync(existing);
        }


        public async Task<QualificationDto> AddQualificationAsync(Guid jobSeekerId, QualificationDto qualificationDto)
        {


            var profile = await _context.JobSeekerProfiles.FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);
            if (profile == null)
                throw new Exception("Job seeker profile not found.");

            var entity = mapper.Map<Qualification>(qualificationDto);
            entity.JobseekerProfileId = profile.Id;
            entity.JobPostId = null;

            var added = await _profileRepository.AddQualificationAsync(entity);
            return mapper.Map<QualificationDto>(added);
        }



        public async Task<QualificationDto?> UpdateQualificationAsync(Guid qualificationId, Guid jobSeekerId, QualificationDto dto)
        {
            var existing = await _profileRepository.GetQualificationByIdAsync(qualificationId);
            if (existing == null)
                throw new KeyNotFoundException("Qualification not found.");


            var profile = await _context.JobSeekerProfiles.FirstOrDefaultAsync(p => p.Id == existing.JobseekerProfileId);
            if (profile == null || profile.JobSeekerId != jobSeekerId)
                throw new UnauthorizedAccessException("You cannot edit another user's qualification.");


            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.JobPostId = null;

            await _profileRepository.UpdateQualificationAsync(existing);

            return mapper.Map<QualificationDto>(existing);
        }


        public async Task<QualificationDto?> PatchQualificationAsync(Guid qualificationId, Guid jobSeekerId, QualificationDto dto)
        {
            var existing = await _profileRepository.GetQualificationByIdAsync(qualificationId);
            if (existing == null)
                throw new KeyNotFoundException("Qualification not found.");


            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.Id == existing.JobseekerProfileId);
            if (profile == null || profile.JobSeekerId != jobSeekerId)
                throw new UnauthorizedAccessException("You cannot edit another user's qualification.");


            if (!string.IsNullOrEmpty(dto.Name))
                existing.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Description))
                existing.Description = dto.Description;


            existing.JobPostId = null;

            await _profileRepository.UpdateQualificationAsync(existing);

            return mapper.Map<QualificationDto>(existing);
        }

        public async Task<IEnumerable<QualificationDto>> GetQualificationsByJobSeekerIdAsync(Guid jobSeekerId)
        {
            var qualifications = await _profileRepository.GetQualificationsByJobSeekerIdAsync(jobSeekerId);
            return mapper.Map<IEnumerable<QualificationDto>>(qualifications);
        }

        public async Task<bool> DeleteQualificationAsync(Guid qualificationId, Guid jobSeekerId)
        {
            var qualification = await _profileRepository.GetQualificationDeleteByIdAsync(qualificationId);
            if (qualification == null)
                throw new KeyNotFoundException("Qualification not found.");


            if (qualification.JobSeekerProfile.JobSeekerId != jobSeekerId)
                throw new UnauthorizedAccessException("You are not allowed to delete this qualification.");

            return await _profileRepository.DeleteQualificationAsync(qualification);
        }

    }
}

