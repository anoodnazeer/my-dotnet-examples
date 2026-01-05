using AutoMapper;
using Domain.Models;
using Domain.Service.Admin.DTOs;

using Domain.Service.Profile.DTOs;

using Domain.Service.JobProvider.DTOs;
using Domain.Service.Login.DTOs;

using Job_Portal.API.Admin.Request_Objects;

namespace Job_Portal.API.Admin.Helper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SkillRequest, SkillDto>();
            CreateMap<SkillDto, Skill>();
            CreateMap<SkillPatchRequest, SkillDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<LocationPatchRequest, LocationDto>();
            CreateMap<LocationDto, Location>();
            CreateMap<CreateJobCategoryDto, JobCategoryDto>();
            CreateMap<JobCategoryDto, JobCategory>();
            CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
            CreateMap<JobCategory,PatchJobCategoryDTO>().ReverseMap();
            CreateMap<JobCategoryDto, PatchJobCategoryDTO>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<LocationRequest, LocationDto>().ReverseMap();
            CreateMap<SystemUser, AdminLoginDTO>().ReverseMap();

            CreateMap<AuthUser, AdminLoginDTO>().ReverseMap();
            CreateMap<AdminLoginRequests, AdminLoginDTO>().ReverseMap();
            CreateMap<AdminLoginDTO, AuthUser>().ReverseMap();
            CreateMap<IndustryObjectDto, Industry>().ReverseMap();
            CreateMap<Industry, IndustryDto>().ReverseMap();
            CreateMap<IndustryObjectDto, IndustryDto>().ReverseMap();
            CreateMap<Industry, IndustryDto>().ReverseMap();
            CreateMap<Industry, PatchIndustryDto>().ReverseMap();
            CreateMap<IndustryDto, PatchIndustryDto>().ReverseMap();

            CreateMap<JobProviderCompany, JobProviderDto>().ReverseMap();
            CreateMap<JobProviderCompany, JobProviderRequestDto>().ReverseMap();
            CreateMap<JobProviderDto, JobProviderRequestDto>().ReverseMap();

            CreateMap<JobPost, JobDto>().ReverseMap();



        }

    }
}
