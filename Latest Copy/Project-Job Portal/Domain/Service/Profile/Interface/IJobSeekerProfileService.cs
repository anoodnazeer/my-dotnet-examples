using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Service.Profile.DTOs;

namespace Domain.Service.Profile.Interface
{
    public interface IJobSeekerProfileService
    {
        Task<JobSeekerProfileDto> CreateProfileAsync(JobSeekerProfileDto JobSeekerProfileDto, Guid jobSeekerId);
        Task<JobSeekerProfileDto> UpdateProfileAsync(JobSeekerProfileDto JobSeekerProfileDto, Guid jobSeekerId);

        Task<JobSeekerProfileDto?> PatchProfileAsync(JobSeekerProfileDto JobSeekerProfileDto, Guid jobSeekerId);

        Task<JobSeekerProfileViewDto?> GetProfileByJobSeekerIdAsync(Guid jobSeekerId);

        Task<string> DeleteProfileAsync(Guid jobSeekerId);

        Task<byte[]?> GetResumeByJobSeekerIdAsync(Guid jobSeekerId);

        Task<List<Skill>> GetAllSkillsAsync();
        Task<string> AddSkillsToJobSeekerAsync(Guid jobSeekerId, List<Guid> skillIds);

        Task<bool> UpdateSkillsAsync(Guid jobSeekerId, List<Guid> newSkillIds);

        Task<bool> PatchSkillsAsync(Guid jobSeekerId, List<Guid> addSkillIds, List<Guid> removeSkillIds);

        Task<bool> DeleteSkillsAsync(Guid jobSeekerId, List<Guid> skillIds);

        Task<List<SkillDto>> GetSkillsByJobSeekerIdAsync(Guid jobSeekerId);

        Task<WorkExperienceDto> AddWorkExperienceAsync(Guid jobSeekerId, WorkExperienceDto dto);

        Task<WorkExperienceDto> UpdateWorkExperienceAsync(Guid jobSeekerId, WorkExperienceDto dto);

        Task<WorkExperienceDto> PatchWorkExperienceAsync(Guid jobSeekerId, WorkExperienceDto dto);

        Task<List<WorkExperienceDto>> GetWorkExperienceByJobSeekerIdAsync(Guid jobSeekerId);

        Task<bool> DeleteWorkExperienceAsync(Guid workExperienceId, Guid jobSeekerId);

        Task<QualificationDto> AddQualificationAsync(Guid jobSeekerId, QualificationDto qualificationDto);

        Task<QualificationDto?> UpdateQualificationAsync(Guid qualificationId, Guid jobSeekerId, QualificationDto dto);

        Task<QualificationDto?> PatchQualificationAsync(Guid qualificationId, Guid jobSeekerId, QualificationDto dto);

        Task<IEnumerable<QualificationDto>> GetQualificationsByJobSeekerIdAsync(Guid jobSeekerId);

        Task<bool> DeleteQualificationAsync(Guid qualificationId, Guid jobSeekerId);
    }
}
