
using AutoMapper;
using Domain_js.Models;
using Domain_js.Service.Login.DTOs;
using Domain_js.Service.SignUp.DTOs;
using JobSeeker.API.JobSeeker.RequestObjects;



namespace JobSeeker.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Domain_js.Models.JobSeeker, Domain_js.Models.AuthUser>().ReverseMap();
            CreateMap<JobSeekerLoginDto, AuthUser>().ReverseMap();
            CreateMap<SignUpRequest, AuthUser>().ReverseMap();
            CreateMap<JobSeekerSignupRequest, JobSeekerSignupRequestDto>().ReverseMap();
            CreateMap<JobSeekerSignupRequestDto, SignUpRequest>().ReverseMap();


        }
    }
}
