using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Service.Profile.DTOs;
using Domain.Service.Profile.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Service.Profile
{
    public class ProfileRepository : IJobSeekerProfileRepository
    {
        protected readonly HireMeNowDbContext _context;
        public ProfileRepository(HireMeNowDbContext context)
        {
            _context = context;
        }

        public async Task<JobSeekerProfile> CreateJobseekerProfileAsync(JobSeekerProfile profile)
        {
            _context.JobSeekerProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<JobSeekerProfile?> GetByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);
        }

        public async Task<JobSeekerProfile> UpdateJobseekerProfileAsync(JobSeekerProfile profile)
        {
            _context.JobSeekerProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<JobSeekerProfile?> ViewProfileByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _context.JobSeekerProfiles
                .Include(p => p.JobSeekerProfileSkills)
                .Include(p => p.Qualifications)
                .Include(p => p.WorkExperiences)
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);
        }

        public async Task<bool> HasAppliedJobsAsync(Guid jobSeekerId)
        {
            return await _context.JobApplications
                .AnyAsync(a => a.ApplicantId == jobSeekerId);
        }

        public async Task<bool> DeleteProfileAsync(Guid jobSeekerId)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                return false;

            _context.JobSeekerProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<byte[]?> GetResumeByJobSeekerIdAsync(Guid jobSeekerId)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            return profile?.Resume;
        }


        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<bool> AddSkillsToJobSeekerAsync(Guid jobSeekerId, List<Guid> skillIds)
        {
            var profile = await _context.JobSeekerProfiles
                .Include(p => p.JobSeekerProfileSkills)
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            // ✅ If no profile exists, create one
            if (profile == null)
            {
                profile = new JobSeekerProfile
                {
                    Id = Guid.NewGuid(),
                    JobSeekerId = jobSeekerId
                };
                _context.JobSeekerProfiles.Add(profile);
            }

            // Remove existing skills
            _context.JobSeekerProfileSkills.RemoveRange(profile.JobSeekerProfileSkills);

            // Add new skills
            foreach (var skillId in skillIds)
            {
                if (await _context.Skills.AnyAsync(s => s.Id == skillId))
                {
                    profile.JobSeekerProfileSkills.Add(new JobSeekerProfileSkill
                    {
                        JobSeekerProfileId = profile.Id,
                        SkillId = skillId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateSkillsAsync(Guid jobSeekerId, List<Guid> newSkillIds)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                return false;


            var existingSkills = await _context.JobSeekerProfileSkills
                .Where(x => x.JobSeekerProfileId == profile.Id)
                .ToListAsync();


            _context.JobSeekerProfileSkills.RemoveRange(existingSkills);


            foreach (var skillId in newSkillIds.Distinct())
            {
                _context.JobSeekerProfileSkills.Add(new JobSeekerProfileSkill
                {
                    JobSeekerProfileId = profile.Id,
                    SkillId = skillId
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> PatchSkillsAsync(Guid jobSeekerId, List<Guid> addSkillIds, List<Guid> removeSkillIds)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                return false;


            if (removeSkillIds != null && removeSkillIds.Any())
            {
                var skillsToRemove = await _context.JobSeekerProfileSkills
                    .Where(x => x.JobSeekerProfileId == profile.Id && removeSkillIds.Contains(x.SkillId))
                    .ToListAsync();

                _context.JobSeekerProfileSkills.RemoveRange(skillsToRemove);
            }


            if (addSkillIds != null && addSkillIds.Any())
            {
                foreach (var skillId in addSkillIds.Distinct())
                {
                    bool exists = await _context.JobSeekerProfileSkills
                        .AnyAsync(x => x.JobSeekerProfileId == profile.Id && x.SkillId == skillId);

                    if (!exists)
                    {
                        _context.JobSeekerProfileSkills.Add(new JobSeekerProfileSkill
                        {
                            JobSeekerProfileId = profile.Id,
                            SkillId = skillId
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteSkillsAsync(Guid jobSeekerId, List<Guid> skillIds)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                return false;

            if (skillIds == null || !skillIds.Any())
                return false;

            var skillsToRemove = await _context.JobSeekerProfileSkills
                .Where(x => x.JobSeekerProfileId == profile.Id && skillIds.Contains(x.SkillId))
                .ToListAsync();

            if (!skillsToRemove.Any())
                return false;

            _context.JobSeekerProfileSkills.RemoveRange(skillsToRemove);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<Skill>> GetSkillsByJobSeekerIdAsync(Guid jobSeekerId)
        {
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            if (profile == null)
                return new List<Skill>();

            var skills = await _context.JobSeekerProfileSkills
                .Include(x => x.Skill)
                .Where(x => x.JobSeekerProfileId == profile.Id)
                .Select(x => x.Skill)
                .ToListAsync();

            return skills;
        }


        public async Task AddWorkExperiencesync(WorkExperience experience)
        {
            await _context.WorkExperiences.AddAsync(experience);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<WorkExperience?> GetWorkExperienceByIdAsync(Guid id)
        {
            return await _context.WorkExperiences.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateWorkExperienceAsync(WorkExperience experience)
        {
            _context.WorkExperiences.Update(experience);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkExperience>> GetBySeekerIdAsync(Guid jobSeekerId)
        {
            return await _context.WorkExperiences
                .Include(w => w.JobSeekerProfile)
                .Where(w => w.JobSeekerProfile.JobSeekerId == jobSeekerId)
                .ToListAsync();
        }

        public async Task<WorkExperience?> GetBySeekerWorkExperienceIdAsync(Guid id)
        {
            return await _context.WorkExperiences
                .Include(w => w.JobSeekerProfile)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> DeleteWorkExperienceAsync(WorkExperience workExperience)
        {
            _context.WorkExperiences.Remove(workExperience);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Qualification> AddQualificationAsync(Qualification qualification)
        {
            qualification.JobPostId = null;

            if (qualification.Id == Guid.Empty)
                qualification.Id = Guid.NewGuid();

            _context.Qualifications.Add(qualification);
            await _context.SaveChangesAsync();
            return qualification;
        }


        public async Task<Qualification?> GetQualificationByIdAsync(Guid id)
        {
            return await _context.Qualifications.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> UpdateQualificationAsync(Qualification qualification)
        {
            _context.Qualifications.Update(qualification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Qualification>> GetQualificationsByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _context.Qualifications
                .Include(q => q.JobSeekerProfile)
                .Where(q => q.JobSeekerProfile.JobSeekerId == jobSeekerId)
                .ToListAsync();
        }

        public async Task<Qualification?> GetQualificationDeleteByIdAsync(Guid qualificationId)
        {
            return await _context.Qualifications
                .Include(q => q.JobSeekerProfile)
                .FirstOrDefaultAsync(q => q.Id == qualificationId);
        }

        public async Task<bool> DeleteQualificationAsync(Qualification qualification)
        {
            _context.Qualifications.Remove(qualification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> JobSeekerProfileExistsAsync(Guid jobSeekerId)
        {
            return await _context.JobSeekerProfiles
                .AnyAsync(p => p.JobSeekerId == jobSeekerId);
        }

    }
}
