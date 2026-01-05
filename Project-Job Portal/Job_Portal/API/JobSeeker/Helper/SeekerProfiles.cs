using AutoMapper;
using Domain.Models;
using Domain.Service.JobSeeker.DTOs;
using Domain.Service.Login.DTOs;
using Job_Portal.API.JobSeeker.RequestObjects;

namespace Job_Portal.Helper
{
    public class SeekerProfiles : Profile
    {
        public SeekerProfiles()
        {
            CreateMap<JobSeekerSignupRequest, JobSeekerSignupRequestDto>().ReverseMap();
            CreateMap<JobSeekerSignupRequestDto, SignUpRequest>().ReverseMap();
            CreateMap<Domain.Models.JobSeeker, AuthUser>().ReverseMap();
            CreateMap<JobSeekerLoginDto, AuthUser>().ReverseMap();
            CreateMap<SignUpRequest, AuthUser>().ReverseMap();
            CreateMap<Domain.Models.SignUpRequest, AuthUser>().ReverseMap();
            CreateMap<ApplyJobRequestDto, JobApplication>();
            CreateMap<ApplyJobRequest, ApplyJobRequestDto>();
            CreateMap<JobApplication, AppliedJobDto>();
            CreateMap<SavedJobDto, SavedJob>();
            CreateMap<SavedJob, SavedJobDto>();
            CreateMap<Interview, InterviewDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.JobTitle))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.LegalName));


        }
    }
}
