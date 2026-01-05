using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Service.Profile.DTOs;

namespace Domain.Service.Profile.Interface
{
    public interface IJobSeekerProfileRepository
    {
        Task<JobSeekerProfile> CreateJobseekerProfileAsync(JobSeekerProfile profile);
        Task<JobSeekerProfile?> GetByJobSeekerIdAsync(Guid jobSeekerId);
        Task<JobSeekerProfile> UpdateJobseekerProfileAsync(JobSeekerProfile profile);
        Task<JobSeekerProfile?> ViewProfileByJobSeekerIdAsync(Guid jobSeekerId);
        Task<bool> HasAppliedJobsAsync(Guid jobSeekerId);
        Task<bool> DeleteProfileAsync(Guid jobSeekerId);
        Task<byte[]?> GetResumeByJobSeekerIdAsync(Guid jobSeekerId);
        Task<List<Skill>> GetAllSkillsAsync();
        Task<bool> AddSkillsToJobSeekerAsync(Guid jobSeekerId, List<Guid> skillIds);
        Task<bool> UpdateSkillsAsync(Guid jobSeekerId, List<Guid> newSkillIds);
        Task<bool> PatchSkillsAsync(Guid jobSeekerId, List<Guid> addSkillIds, List<Guid> removeSkillIds);
        Task<bool> DeleteSkillsAsync(Guid jobSeekerId, List<Guid> skillIds);
        Task<List<Skill>> GetSkillsByJobSeekerIdAsync(Guid jobSeekerId);
        Task AddWorkExperiencesync(WorkExperience experience);
        Task SaveChangesAsync();
        Task<WorkExperience?> GetWorkExperienceByIdAsync(Guid id);
        Task UpdateWorkExperienceAsync(WorkExperience experience);
        Task<List<WorkExperience>> GetBySeekerIdAsync(Guid jobSeekerId);
        Task<WorkExperience?> GetBySeekerWorkExperienceIdAsync(Guid id);
        Task<bool> DeleteWorkExperienceAsync(WorkExperience workExperience);
        Task<Qualification> AddQualificationAsync(Qualification qualification);
        Task<Qualification?> GetQualificationByIdAsync(Guid id);
        Task<bool> UpdateQualificationAsync(Qualification qualification);
        Task<IEnumerable<Qualification>> GetQualificationsByJobSeekerIdAsync(Guid jobSeekerId);
        Task<Qualification?> GetQualificationDeleteByIdAsync(Guid qualificationId);
        Task<bool> DeleteQualificationAsync(Qualification qualification);
        Task<bool> JobSeekerProfileExistsAsync(Guid jobSeekerId);

    }
}
