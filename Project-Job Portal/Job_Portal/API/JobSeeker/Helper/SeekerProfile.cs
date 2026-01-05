using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Service.JobSeeker.DTOs;
using Domain.Service.Profile.DTOs;
using Job_Portal.API.JobSeeker.RequestObjects;

namespace Job_Portal.API.JobSeeker.Helper
{ 
    public class SeekerProfile :Profile
    {
        public SeekerProfile()
        {
            CreateMap<CreateJobSeekerProfileRequest, JobSeekerProfileDto>();
            CreateMap<UpdateJobSeekerProfileRequest, JobSeekerProfileDto>();
            CreateMap<PatchJobSeekerProfileRequest, JobSeekerProfileDto>();
            CreateMap<AddJobSeekerSkillsRequest, JobseekerProfileSkillDto>();
            CreateMap<UpdateSkillsRequest, JobseekerProfileSkillDto>();
            CreateMap<Skill, SkillDto>();
            CreateMap<AddWorkExperienceRequest, WorkExperienceDto>();
            CreateMap<WorkExperienceDto, WorkExperience>();
            CreateMap<WorkExperience, WorkExperienceDto>();
            CreateMap<UpdateWorkExperienceRequest, WorkExperienceDto>();
            CreateMap<PatchWorkExperienceRequest, WorkExperienceDto>();
            CreateMap<QualificationAddRequest, QualificationDto>();
            CreateMap<QualificationDto, Qualification>();
            CreateMap<Qualification, QualificationDto>();
            CreateMap<QualificationUpdateRequest, QualificationDto>();
            CreateMap<QualificationPatchRequest, QualificationDto>();


        }
    }
}
